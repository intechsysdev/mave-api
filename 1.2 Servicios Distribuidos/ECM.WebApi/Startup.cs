using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using ECM.Aplicacion.DTO.Gupshup;
using ECM.Aplicacion.Servicios.Authentication;
using ECM.Aplicacion.Servicios.Interfaz.Authentication;
using ECM.Aplicacion.Servicios.Interfaz.Messages;
using ECM.Aplicacion.Servicios.Interfaz.ModuloApp;
using ECM.Aplicacion.Servicios.Interfaz.ModuloCliente;
using ECM.Aplicacion.Servicios.Interfaz.ModuloEcommerce;
using ECM.Aplicacion.Servicios.Interfaz.ModuloMob;
using ECM.Aplicacion.Servicios.Interfaz.ModuloSeg;
using ECM.Aplicacion.Servicios.Messages;
using ECM.Aplicacion.Servicios.ModuloApp;
using ECM.Aplicacion.Servicios.ModuloCliente;
using ECM.Aplicacion.Servicios.ModuloEcommerce;
using ECM.Aplicacion.Servicios.ModuloMob;
using ECM.Aplicacion.Servicios.ModuloSeg;
using ECM.Dominio.ModuloMob.Repositories;
using ECM.Dominio.ModuloSeg.Repositories;
using ECM.Dominio.UnitsOfWork;
using ECM.Infraestructura.Datos.Repositories.ModuloMob;
using ECM.Infraestructura.Datos.Repositories.ModuloSeg;
using ECM.Infraestructura.Datos.UnidadTrabajo;
using IBM.EntityFrameworkCore;
using Itdear.Infraestructura.Seguridad.JWT;
using Itdear.Infraestructura.Transversal.Adaptador;
using Itdear.Infraestructura.Transversal.ContextAccessor;
using Itdear.ServiciosDistribuidos.WebApi.Core.Middlewares;
using Itdear.Transversal.NetCore.Adaptador;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace ECM.WebApi
{
    public class Startup
    {
        private const string SpecificOrigins = "specificOrigins";

        /// <summary>Cultura de la aplicacion. Define el formato de fechas y numeros de las respuestas.</summary>
        private static readonly CultureInfo Cultura = new CultureInfo("es-CO");

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHttpClient();
            services.AddHttpContextAccessor();
            services.TryAddSingleton<IActionContextAccessor, ActionContextAccessor>();

            services.AddSingleton<IContextAccessor, ContextAccessor>();

            #region Configuracion Log

            services.AddScoped(typeof(IAppLogger<>), typeof(LoggerAdapter<>));

            #endregion

            #region Messages

            services.AddScoped<IMailService, MailService>();
            services.AddScoped<ISmsService, SmsService>();

            // Integracion WhatsApp / Gupshup
            services.Configure<GupshupSettings>(options => Configuration.GetSection("Gupshup").Bind(options));
            services.AddScoped<IWhatsappTemplateData, WhatsappTemplateData>();
            services.AddScoped<IGupshupService, GupshupService>();

            #endregion

            #region Configuracion AutoMapper

            // El adaptador se registra tambien en el punto de acceso estatico porque los
            // metodos ProjectedAs / ProjectedAsCollection se invocan desde entidades
            // sueltas, donde no hay inyeccion de dependencias disponible.
            var typeAdapterFactory = new AutomapperTypeAdapterFactory();

            services.AddSingleton<ITypeAdapterFactory>(typeAdapterFactory);

            TypeAdapterFactory.SetCurrent(typeAdapterFactory);

            #endregion

            #region Configuracion Autenticacion y Autorizacion JWT

            var jwtOptions = Configuration.GetSection(nameof(JwtOptions));

            var secretKey = Configuration["SecretKeyJWT"];

            if (string.IsNullOrWhiteSpace(secretKey))
            {
                throw new InvalidOperationException(
                    "No se encontro la clave SecretKeyJWT. Definala como variable de entorno o en la configuracion de la aplicacion.");
            }

            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));

            services.Configure<JwtOptions>(options =>
            {
                options.Issuer = jwtOptions[nameof(JwtOptions.Issuer)];
                options.Audience = jwtOptions[nameof(JwtOptions.Audience)];
                options.ValidForMinutes = int.Parse(
                    jwtOptions[nameof(JwtOptions.ValidForMinutes)], CultureInfo.InvariantCulture);
                options.SigningCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);
            });

            services.AddScoped<IJwtFactory, JwtFactory>();

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = jwtOptions[nameof(JwtOptions.Issuer)],

                ValidateAudience = true,
                ValidAudience = jwtOptions[nameof(JwtOptions.Audience)],

                ValidateIssuerSigningKey = true,
                IssuerSigningKey = signingKey,

                RequireExpirationTime = false,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(configureOptions =>
            {
                configureOptions.ClaimsIssuer = jwtOptions[nameof(JwtOptions.Issuer)];
                configureOptions.TokenValidationParameters = tokenValidationParameters;
                configureOptions.SaveToken = true;

                // Los claims propios (CompanyId, Cust, Succli) se leen por su nombre
                // original, sin la traduccion a uris de WS-Security.
                configureOptions.MapInboundClaims = false;
            });

            services.AddAuthorization();

            services.AddScoped<IAuthenticationAppService, AuthenticationAppService>();

            #endregion

            #region Configuracion unidades de trabajo

            services.AddDbContext<UnitOfWorkECM>(options => options
                .EnableSensitiveDataLogging(false)
                .UseDb2(Configuration.GetConnectionString("UnitOfWorkECM"), p =>
                {
                    p.SetServerInfo(IBMDBServerType.IDS, IBMDBServerVersion.IDS_12_10_2000);
                    p.UseRowNumberForPaging();
                }));

            services.AddTransient<IUnitOfWorkECM>(provider => provider.GetRequiredService<UnitOfWorkECM>());

            services.AddDbContext<UnitOfWorkSEG>(options => options
                .EnableSensitiveDataLogging(false)
                .UseDb2(Configuration.GetConnectionString("UnitOfWorkSEG"), p =>
                {
                    p.SetServerInfo(IBMDBServerType.IDS, IBMDBServerVersion.IDS_12_10_2000);
                    p.UseRowNumberForPaging();
                }));

            services.AddTransient<IUnitOfWorkSEG>(provider => provider.GetRequiredService<UnitOfWorkSEG>());

            #endregion

            #region ModuloMob

            services.AddScoped<IGestionModAppService, GestionModAppService>();

            services.AddScoped<IMobCarteraRepository, MobCarteraRepository>();
            services.AddScoped<IMobEventosItemsRepository, MobEventosItemsRepository>();
            services.AddScoped<IMobPromocionesRepository, MobPromocionesRepository>();
            services.AddScoped<IMobRcustConsecRepository, MobRcustConsecRepository>();
            services.AddScoped<IMobRcustUserRepository, MobRcustUserRepository>();
            services.AddScoped<IMobRlineaRepository, MobRlineaRepository>();
            services.AddScoped<IMobRmenuPpalRepository, MobRmenuPpalRepository>();
            services.AddScoped<IMobRordHeadCustRepository, MobRordHeadCustRepository>();
            services.AddScoped<IMobRordLineCustRepository, MobRordLineCustRepository>();
            services.AddScoped<IMobRordLineCustShopRepository, MobRordLineCustShopRepository>();
            services.AddScoped<IMobRperfilesRepository, MobRperfilesRepository>();
            services.AddScoped<IMobRperfilMenuPpalRepository, MobRperfilMenuPpalRepository>();
            services.AddScoped<IMobRprodCustRepository, MobRprodCustRepository>();
            services.AddScoped<IMobRproductosRepository, MobRproductosRepository>();
            services.AddScoped<IMobRsubLineasRepository, MobRsubLineasRepository>();
            services.AddScoped<IMobRsubMenuRepository, MobRsubMenuRepository>();
            services.AddScoped<IMobCalendarioRepository, MobCalendarioRepository>();
            services.AddScoped<IMobRusuariosRepository, MobRusuariosRepository>();

            #endregion

            #region ModuloSeg

            services.AddScoped<IGestionSegAppService, GestionSegAppService>();

            services.AddScoped<IEcmMcontactoRepository, EcmMcontactoRepository>();
            services.AddScoped<IEcmMempresaRepository, EcmMempresaRepository>();
            services.AddScoped<IEcmMmonedaRepository, EcmMmonedaRepository>();
            services.AddScoped<IEcmMnappRepository, EcmMnappRepository>();
            services.AddScoped<IEcmMtappRepository, EcmMtappRepository>();
            services.AddScoped<IEcmMusuarioRepository, EcmMusuarioRepository>();
            services.AddScoped<IEcmMvappRepository, EcmMvappRepository>();
            services.AddScoped<IEcmPaisRepository, EcmPaisRepository>();
            services.AddScoped<IEcmRaccesoRepository, EcmRaccesoRepository>();
            services.AddScoped<IEcmRmacRepository, EcmRmacRepository>();
            services.AddScoped<IEcmRlogusuarioRepository, EcmRlogusuarioRepository>();
            services.AddScoped<IEcmRnappRepository, EcmRnappRepository>();
            services.AddScoped<IEcmRpasswordRepository, EcmRpasswordRepository>();
            services.AddScoped<IEcmRterminosusoRepository, EcmRterminosusoRepository>();

            #endregion

            #region Modulo Cliente

            services.AddScoped<IModuloClienteAppService, ModuloClienteAppService>();

            #endregion

            #region Modulo ECommerce

            services.AddScoped<IModuloEcommerceAppService, ModuloEcommerceAppService>();

            #endregion

            #region Modulo App

            services.AddScoped<IModuloAppAppService, ModuloAppAppService>();

            #endregion

            services.AddMemoryCache();

            var corsOrigins = Configuration.GetSection("CorsOrigins").Get<string[]>() ?? Array.Empty<string>();

            services.AddCors(options =>
            {
                options.AddPolicy(SpecificOrigins, policy =>
                {
                    policy.WithOrigins(corsOrigins).AllowAnyHeader().AllowAnyMethod();
                });
            });

            services.Configure<RequestLocalizationOptions>(options =>
            {
                var culturas = new List<CultureInfo> { Cultura };

                options.DefaultRequestCulture = new RequestCulture(Cultura, Cultura);
                options.SupportedCultures = culturas;
                options.SupportedUICultures = culturas;
            });

            services.AddLogging(logging =>
            {
                logging.AddConfiguration(Configuration.GetSection("Logging"));
                logging.AddConsole();
                logging.AddDebug();
            });

            services.AddControllers();
        }

        public void Configure(WebApplication app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // El valor por defecto de HSTS son 30 dias. Revise este valor para produccion:
                // https://aka.ms/aspnetcore-hsts
                app.UseHsts();
            }

            // Va lo mas arriba posible del pipeline para poder traducir a json cualquier
            // excepcion que se produzca aguas abajo.
            app.UseMiddleware<CustomExceptionMiddleware>();

            app.UseHttpsRedirection();

            app.UseStaticFiles(new StaticFileOptions
            {
                OnPrepareResponse = NoCache
            });

            app.UseRouting();

            app.UseCors(SpecificOrigins);

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseRequestLocalization();

            app.MapControllers();

            // Todo lo que no sea del api se sirve como la SPA, para que el enrutamiento
            // del cliente funcione al recargar una url profunda.
            app.MapWhen(
                context => !context.Request.Path.StartsWithSegments("/api", StringComparison.OrdinalIgnoreCase),
                spa =>
                {
                    spa.Use((context, next) =>
                    {
                        context.Request.Path = new PathString("/index.html");

                        return next();
                    });

                    spa.UseStaticFiles(new StaticFileOptions
                    {
                        OnPrepareResponse = NoCache
                    });
                });
        }

        /// <summary>
        /// Impide que el navegador cachee los archivos de la SPA: asi un despliegue nuevo
        /// se toma sin tener que limpiar la cache del cliente.
        /// </summary>
        private static void NoCache(StaticFileResponseContext context)
        {
            var headers = context.Context.Response.Headers;

            headers["Cache-Control"] = "no-cache, no-store, must-revalidate";
            headers["Expires"] = "0";
            headers["Pragma"] = "no-cache";
        }
    }
}
