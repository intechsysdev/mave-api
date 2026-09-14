using System.Globalization;
using System.Text;
using IBM.EntityFrameworkCore;
using IBM.EntityFrameworkCore.Storage.Internal;
using Itdear.Infraestructura.Seguridad.JWT;
using Itdear.Infraestructura.Transversal.Adaptador;
using Itdear.Infraestructura.Transversal.ContextAccessor;
using Itdear.ServiciosDistribuidos.WebApi.Core.Middlewares;
using Itdear.Transversal.NetCore.Adaptador;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using ECM.Aplicacion.Servicios.Interfaz.ModuloMob;
using ECM.Aplicacion.Servicios.ModuloMob;
using ECM.Dominio.ModuloMob.Repositories;
using ECM.Dominio.UnitsOfWork;
using ECM.Infraestructura.Datos.Repositories.ModuloMob;
using ECM.Infraestructura.Datos.UnidadTrabajo;
using ECM.Aplicacion.Servicios.Interfaz.ModuloSeg;
using ECM.Dominio.ModuloSeg.Repositories;
using ECM.Infraestructura.Datos.Repositories.ModuloSeg;
using ECM.Aplicacion.Servicios.ModuloSeg;
using System;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using ECM.Aplicacion.Servicios.Interfaz.Authentication;
using ECM.Aplicacion.Servicios.Authentication;
using ECM.Aplicacion.Servicios.Messages;
using ECM.Aplicacion.Servicios.Interfaz.ModuloCliente;
using ECM.Aplicacion.Servicios.ModuloCliente;
using ECM.Aplicacion.Servicios.Interfaz.ModuloEcommerce;
using ECM.Aplicacion.Servicios.ModuloEcommerce;
using ECM.Aplicacion.Servicios.Interfaz.ModuloApp;
using ECM.Aplicacion.Servicios.ModuloApp;
using ECM.Aplicacion.Servicios.Interfaz.Messages;
using Microsoft.AspNetCore.Http;

namespace ECM.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        readonly string SpecificOrigins = "specificOrigins";

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            var cultureInfo = new CultureInfo("es-CO");
            CultureInfo.CurrentCulture = cultureInfo;

            services.AddHttpClient();
            services.AddHttpContextAccessor();
            services.TryAddSingleton<IActionContextAccessor, ActionContextAccessor>();

            services.AddSingleton<IContextAccessor, ContextAccessor>();

            #region Configuración Log

            services.AddScoped(typeof(IAppLogger<>), typeof(LoggerAdapter<>));

            #endregion

            #region Messages

            services.AddScoped<IMailService, MailService>();
            services.AddScoped<ISmsService, SmsService>();

            #endregion

            #region Configuración AutoMapper

            services.AddScoped<ITypeAdapterFactory, AutomapperTypeAdapterFactory>();

            var serviceProvider = services.BuildServiceProvider();
            var adaptador = serviceProvider.GetRequiredService<ITypeAdapterFactory>();
            TypeAdapterFactory.SetCurrent(adaptador);

            #endregion

            #region Configuración Autenticación y Autorización JWT

            var jwtOptions = Configuration.GetSection(nameof(JwtOptions));

            services.Configure<JwtOptions>(options =>
            {
                options.Issuer = jwtOptions[nameof(JwtOptions.Issuer)];
                options.Audience = jwtOptions[nameof(JwtOptions.Audience)];
                options.ValidForMinutes = int.Parse(jwtOptions[nameof(JwtOptions.ValidForMinutes)], CultureInfo.InvariantCulture);
                options.SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(Configuration["SecretKeyJWT"])), SecurityAlgorithms.HmacSha256);
            });

            services.AddScoped<IJwtFactory, JwtFactory>();

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = jwtOptions[nameof(JwtOptions.Issuer)],

                ValidateAudience = true,
                ValidAudience = jwtOptions[nameof(JwtOptions.Audience)],

                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["SecretKeyJWT"])),

                RequireExpirationTime = false,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).
            AddJwtBearer(configureOptions =>
            {
                configureOptions.ClaimsIssuer = jwtOptions[nameof(JwtOptions.Issuer)];
                configureOptions.TokenValidationParameters = tokenValidationParameters;
                configureOptions.SaveToken = true;
            });

            services.AddScoped<IAuthenticationAppService, AuthenticationAppService>();

            #endregion

            #region Configuración unidades de trabajo

            services.AddDbContext<UnitOfWorkECM>(options => options.EnableSensitiveDataLogging(false).UseDb2(Configuration.GetConnectionString("UnitOfWorkECM"), p => { p.SetServerInfo(IBMDBServerType.IDS, IBMDBServerVersion.IDS_12_10_2000); p.UseRowNumberForPaging(); }));

            services.AddTransient<IUnitOfWorkECM>(provider => provider.GetService<UnitOfWorkECM>());

            services.AddDbContext<UnitOfWorkSEG>(options => options.EnableSensitiveDataLogging(false).UseDb2(Configuration.GetConnectionString("UnitOfWorkSEG"), p => { p.SetServerInfo(IBMDBServerType.IDS, IBMDBServerVersion.IDS_12_10_2000); p.UseRowNumberForPaging(); }));

            services.AddTransient<IUnitOfWorkSEG>(provider => provider.GetService<UnitOfWorkSEG>());
                        
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

            #region  Modulo Cliente
            services.AddScoped<IModuloClienteAppService, ModuloClienteAppService>();

            #endregion

            #region  Modulo ECommerce
            services.AddScoped<IModuloEcommerceAppService, ModuloEcommerceAppService>();

            #endregion

            #region  Modulo App
            services.AddScoped<IModuloAppAppService, ModuloAppAppService>();

            #endregion

            // Add memory cache services
            services.AddMemoryCache();

            var corsOrigins = Configuration.GetSection("CorsOrigins").Get<string[]>();

            services.AddCors(options =>
            {
                options.AddPolicy(SpecificOrigins,
                builder =>
                {
                    builder.WithOrigins(corsOrigins).AllowAnyHeader().AllowAnyMethod();
                });
            });


            services.AddLogging(logging =>
            {
                logging.AddConfiguration(Configuration.GetSection("Logging"));
                logging.AddConsole();
                logging.AddDebug();
            });


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }


            app.UseStaticFiles(new StaticFileOptions
            {
                OnPrepareResponse = context =>
                {
                    context.Context.Response.Headers.Add("Cache-Control", "no-cache, no-store, must-revalidate");
                    context.Context.Response.Headers.Add("Expires", "0");
                    context.Context.Response.Headers.Add("Pragma", "no-cache");
                }
            });

            app.UseCors(SpecificOrigins);

            app.UseAuthentication();

            app.UseHttpsRedirection();

            app.UseMiddleware<CustomExceptionMiddleware>();

            app.UseRequestLocalization();

            app.UseMvc();

            app.MapWhen(x => !x.Request.Path.Value.StartsWith("/api"), builder =>
            {
                builder.Use((context, next) =>
                {
                    context.Request.Path = new PathString("/index.html");
                    return next();
                });

                builder.UseStaticFiles(new StaticFileOptions
                {
                    OnPrepareResponse = context =>
                    {
                        context.Context.Response.Headers.Add("Cache-Control", "no-cache, no-store, must-revalidate");
                        context.Context.Response.Headers.Add("Expires", "0");
                        context.Context.Response.Headers.Add("Pragma", "no-cache");
                    }
                });

            });
        }
    }
}
