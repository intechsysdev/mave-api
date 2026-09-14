using ECM.Aplicacion.DTO.Authentication;
using ECM.Aplicacion.DTO.Messages;
using ECM.Aplicacion.Servicios.Interfaz.Authentication;
using ECM.Aplicacion.Servicios.Interfaz.Messages;
using ECM.Dominio.ModuloSeg.Entities;
using ECM.Dominio.ModuloSeg.Repositories;
using ECM.Dominio.UnitsOfWork;
using Itdear.Infraestructura.Seguridad.Cryptography;
using Itdear.Infraestructura.Seguridad.JWT;
using Itdear.Infraestructura.Transversal.ContextAccessor;
using Itdear.Infraestructura.Transversal.Exception;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ECM.Aplicacion.Servicios.Authentication
{
    public class AuthenticationAppService : IAuthenticationAppService
    {
        private readonly IUnitOfWorkSEG _unitOfWorkSEG;
        private readonly IContextAccessor _contextAccessor;
        private readonly IJwtFactory _jwtFactory;
        private readonly ILogger _logger;
        private readonly IEcmMusuarioRepository _ecmMusuarioRepository;
        private readonly IEcmMempresaRepository _ecmMempresaRepository;
        private readonly IEcmRaccesoRepository _ecmRaccesoRepository;
        private readonly IEcmRmacRepository _ecmRmacRepository;
        private readonly IEcmRterminosusoRepository _ecmRterminosusoRepository;
        private readonly IMailService _mailService;
        private readonly ISmsService _smsService;
        private readonly IConfiguration _configuration;

        public AuthenticationAppService(
           IUnitOfWorkSEG unitOfWorkSEG,
           IContextAccessor contextAccessor,
           IJwtFactory jwtFactory,
           IMailService mailService,
           ISmsService smsService,
           ILogger<AuthenticationAppService> logger,
           IEcmMusuarioRepository ecmMusuarioRepository,           
           IEcmMempresaRepository ecmMempresaRepository,
           IEcmRaccesoRepository ecmRaccesoRepository,
           IEcmRmacRepository ecmRmacRepository,
           IEcmRterminosusoRepository ecmRterminosusoRepository,
           IConfiguration configuration
           )
        {
            _unitOfWorkSEG = unitOfWorkSEG;
            _contextAccessor = contextAccessor;
            _jwtFactory = jwtFactory;
            _logger = logger;
            _configuration = configuration;
            _mailService = mailService;
            _smsService = smsService;
            _ecmMusuarioRepository = ecmMusuarioRepository;
            _ecmMempresaRepository = ecmMempresaRepository;
            _ecmRaccesoRepository = ecmRaccesoRepository;
            _ecmRmacRepository = ecmRmacRepository;
            _ecmRterminosusoRepository = ecmRterminosusoRepository;
        }


        public async Task<LoginResultDTO> Login(LoginDTO login, string codigoVerificacion = null)
        {
            string msgValidacion = "";
            var usuario = await _ecmMusuarioRepository.Query(q => q.MusuaEmprId == "VE" && q.MusuaCust == login.UserCust && q.MusuaSuccli == login.UserSuccli).FirstOrDefaultAsync();

            if (usuario == null)
            {
                msgValidacion = "Usuario no valido. Verifique los datos";
            }
            else
            {
                if (usuario.MusuaPassEncrip != new SegMD5().GetMd5Hash(login.Password))
                {
                    msgValidacion = $"Contraseña no valida. Verifique los datos.";
                }
                else if (usuario.MusuaApprove.Trim().ToLower() != "s")
                {
                    throw new ValidationException("La cuenta se encuentra inactiva. Contacte con soporte.");
                }               
            }

            if (usuario != null && string.IsNullOrWhiteSpace(msgValidacion))
            {
                var requiere2FactorAut = false;

                if (!string.IsNullOrWhiteSpace(codigoVerificacion))
                {
                    if (!Valida2FactorAutenticacion(usuario, codigoVerificacion))
                    {
                        throw new ValidationException("Código de verificación no valido.");
                    }
                }
                else {
                    requiere2FactorAut = await Requiere2FactorAutenticacion(usuario, true);
                }

                var terminosCondiciones = await _ecmRterminosusoRepository
                .Query(q => q.RtermEmprId == usuario.MusuaEmprId && q.RtermUsuaCust == usuario.MusuaCust && q.RtermUsuaSuccli == usuario.MusuaSuccli && q.RtermMac == _contextAccessor.ClientIP)
                .FirstOrDefaultAsync();

                var idSesion = Guid.NewGuid().ToString();

                var empresa = await _ecmMempresaRepository
                        .Query(q => q.MemprCmpy == usuario.MusuaEmprId)
                        .FirstOrDefaultAsync();

                if (empresa == null)
                {
                    throw new ValidationException("Empresa no valida, contacte con soporte.");
                }

                LoginResultDTO result = new LoginResultDTO()
                {
                    User = new UserLoginDTO()
                    {
                        CompanyId = usuario.MusuaEmprId,
                        CompanyName = empresa.MemprName,
                        UserCust = usuario.MusuaCust,
                        UserSuccli = usuario.MusuaSuccli,
                        UserName = usuario.MusuaName,
                        FirstName = usuario.MusuaName,
                        EmailAddress = usuario.MusuaMail,
                        Phone = usuario.MusuaPhone,
                        ApprovedTerms = terminosCondiciones != null && terminosCondiciones.RtermApprove.Trim().ToLower() == "s"
                    },
                    TwoFactorAuth = requiere2FactorAut,
                    Valid = true
                };

                if (!requiere2FactorAut)
                {
                    result.Company = new CompanyDTO
                    {
                        CompanyId = empresa.MemprId,
                        Terms = empresa.MemprTerm,
                        FormatDate = empresa.MemprFdate,
                        FormatCurrency = empresa.MemprFcurr,
                        FormatNumber = empresa.MemprFnumb,
                        NameCompany = empresa.MemprName,
                        WebCompany = empresa.MemprWeb,
                        AddressCompany = empresa.MemprAddr,
                        PhoneCompany = empresa.MemprPhon,
                        UrlApi = empresa.MemprConn
                    };

                    result.Valid = true;
                    result.TokenSession = idSesion;
                    result.Jwt = BuildToken(result);

                    RegistrarAcceso(usuario);
                }

                _unitOfWorkSEG.SaveChanges();

                return result;
            }
            else
            {
                throw new ValidationException(msgValidacion);
            }
        }

        private void RegistrarAcceso(EcmMusuario usuario) {
            var registro = new EcmRacceso
            {
                RacceEmprId = usuario.MusuaEmprId,
                RacceUsuaCust = usuario.MusuaCust,
                RacceUsuaSuccli = usuario.MusuaSuccli,
                RacceMac = _contextAccessor.ClientIP,
                RacceVappId = _contextAccessor.AppVersion,
                RacceTappId = _contextAccessor.AppType,
                RacceDate = DateTime.Now                
            };
            _ecmRaccesoRepository.Insert(registro);
        }

        private async Task<bool> Requiere2FactorAutenticacion(EcmMusuario usuario, bool siempreEnviarCodigo)
        {

            var empresa = await _ecmMempresaRepository.Query(q => q.MemprCmpy == usuario.MusuaEmprId).FirstOrDefaultAsync();

            if (empresa == null)
            {
                throw new Exception("Empresa no valida");
            }

            var mac = await _ecmRmacRepository
                .Query(q => q.RmacEmprId == usuario.MusuaEmprId && q.RmacUsuaCust == usuario.MusuaCust && q.RmacUsuaSuccli == usuario.MusuaSuccli && q.RmacMac == _contextAccessor.ClientIP)
                .FirstOrDefaultAsync();


            int codigoVerificacion = new Random().Next(100000, 999999);

            if (mac == null)
            {
                await EnviarMensajeCodigoVerifacion(usuario, empresa, codigoVerificacion);

                EcmRmac ecmRmac = new EcmRmac()
                {
                    RmacEmprId = usuario.MusuaEmprId,
                    RmacUsuaCust = usuario.MusuaCust,
                    RmacUsuaSuccli = usuario.MusuaSuccli,
                    RmacMac = _contextAccessor.ClientIP,
                    RmacDate = DateTime.Now,
                    RmacVapp2fa = new SegMD5().GetMd5Hash(codigoVerificacion.ToString())
                };

                _ecmRmacRepository.Insert(ecmRmac);
            } else if (siempreEnviarCodigo)
            {
                await EnviarMensajeCodigoVerifacion(usuario, empresa, codigoVerificacion);

                mac.RmacDate = DateTime.Now;
                mac.RmacVapp2fa = new SegMD5().GetMd5Hash(codigoVerificacion.ToString());
                _ecmRmacRepository.Update(mac);
            }
            else {
                return false;
            }           

            return true;
        }

        private async Task EnviarMensajeCodigoVerifacion(EcmMusuario usuario, EcmMempresa empresa, int codigoVerificacion)
        {
            var telefono = $"{empresa.MemprePaisId}{usuario.MusuaPhone}";

            var parametersMsg = _configuration.GetSection("FactorAuthenticationMsg").Get<ParametersMsgDTO>();

            var msgSms = parametersMsg.TextSms.Replace("{code}", codigoVerificacion.ToString());

            await _smsService.Send(empresa.MemprApisms, parametersMsg.FromSms, telefono, msgSms);

            await _mailService.Send(empresa.MemprApisms, parametersMsg.FromMail, usuario.MusuaMail, parametersMsg.Subject, parametersMsg.TemplateId, new { Usuario = usuario.MusuaName, Correo = usuario.MusuaMail, codesecurityecommerce = codigoVerificacion }, generateException: true);
            
            _logger.LogInformation($"Codigo verificación {codigoVerificacion}");
        }

        private bool Valida2FactorAutenticacion(EcmMusuario usuario, string codigoVerificacion)
        {
            var mac = _ecmRmacRepository
                .Query(q => q.RmacEmprId == usuario.MusuaEmprId && q.RmacUsuaCust == usuario.MusuaCust && q.RmacUsuaSuccli == usuario.MusuaSuccli && q.RmacMac == _contextAccessor.ClientIP)
                .FirstOrDefault();

            if (mac != null && mac.RmacVapp2fa == new SegMD5().GetMd5Hash(codigoVerificacion))
            {
                mac.RmacVapp2fa = "";
                return true;
            }
                
            return false;
        }

        public async Task<LoginResultDTO> ValidationPassword(LoginDTO login)
        {
            string msgValidacion = "";
            var usuario = await _ecmMusuarioRepository.Query(q => q.MusuaEmprId == "VE" && q.MusuaCust == login.UserCust && q.MusuaSuccli == login.UserSuccli).FirstOrDefaultAsync();

            if (usuario == null)
            {
                msgValidacion = "Usuario no valido. Verifique los datos";
            }
            else if (usuario.MusuaApprove.Trim().ToLower() != "s")
            {
                throw new ValidationException("La cuenta se encuentra inactiva. Contacte con soporte.");
            }
            else
            {
                if (usuario.MusuaPassEncrip != new SegMD5().GetMd5Hash(login.Password))
                {
                    msgValidacion = $"Contraseña no valida. Verifique los datos.";
                }
            }

            if (usuario != null && string.IsNullOrWhiteSpace(msgValidacion))
            {
                LoginResultDTO result = new LoginResultDTO()
                {
                    User = new UserLoginDTO()
                    {
                        UserCust = usuario.MusuaCust,
                        UserSuccli = usuario.MusuaCust,
                        UserName = usuario.MusuaName,
                        FirstName = usuario.MusuaName,
                        EmailAddress = usuario.MusuaMail,
                    },
                    Valid = true,
                };
                return result;
            }
            else
            {
                throw new ValidationException(msgValidacion);
            }
        }

        public async Task AssingPassword(AssingPasswordDTO assingPassword)
        {
            var usuario = await _ecmMusuarioRepository.Query(q => q.MusuaEmprId == "VE" && q.MusuaCust == assingPassword.UserCust && q.MusuaSuccli == assingPassword.UserSuccli).FirstOrDefaultAsync();

            if (usuario == null)
            {
                throw new ValidationException("Token o usuario no valido");
            }

            if (string.IsNullOrEmpty(usuario.MusuaToken))
            {
                throw new ValidationException("Ya se utilizó el código de recuperación");
            }

            if (usuario.MusuaMail == null || (usuario.MusuaMail.ToLower().Trim() != assingPassword.Email.ToLower().Trim()))
            {
                throw new ValidationException("El correo electrónico no corresponde al registrado para el usuario. Verifique la información.");
            }

            if (!string.IsNullOrEmpty(usuario.MusuaToken) && usuario.MusuaToken != new SegMD5().GetMd5Hash(assingPassword.Token))
            {
                throw new ValidationException("Token o usuario no valido");
            }

            if (usuario.MusuaExpirationToken < DateTime.Now)
            {
                throw new ValidationException("El token ha vencido. Genere un nuevo token en recuperar contraseña");
            }

            if (assingPassword.Password != assingPassword.ConfirmPassword)
            {
                throw new ValidationException("La contraseña y confirmación de contraseña no son iguales. Verifique la información e intentelo de nuevo");
            }

            if (usuario.MusuaApprove.Trim().ToLower() != "s")
            {
                throw new ValidationException("La cuenta se encuentra inactiva. Contacte con soporte.");
            }

            usuario.MusuaPassEncrip = new SegMD5().GetMd5Hash(assingPassword.Password);
            usuario.MusuaToken = null;
            usuario.MusuaExpirationToken = null;
            
            await _unitOfWorkSEG.SaveChangesAsync();
        }

        public async Task ChangePassword(ChangePasswordDTO changePassword)
        {
            var usuario = await _ecmMusuarioRepository.Query(q => q.MusuaEmprId == "VE" && q.MusuaCust == _contextAccessor.UserCust && q.MusuaSuccli == _contextAccessor.UserSuccli).FirstOrDefaultAsync();

            if (usuario == null)
            {
                throw new ValidationException("Token o usuario no valido");
            }

            if (string.IsNullOrEmpty(usuario.MusuaPassEncrip) && string.IsNullOrEmpty(usuario.MusuaToken))
            {
                throw new ValidationException("Datos no validos");
            }

            if (!string.IsNullOrEmpty(usuario.MusuaPassEncrip) && usuario.MusuaPassEncrip != new SegMD5().GetMd5Hash(changePassword.OldPassword))
            {
                throw new ValidationException("Contraseña o usuario no valido");
            }

            if (usuario.MusuaExpirationToken < DateTime.Now)
            {
                throw new ValidationException("El token ha vencido. Genere un nuevo token en recuperar contraseña");
            }

            if (changePassword.Password != changePassword.ConfirmPassword)
            {
                throw new ValidationException("La constreña y confirmación de contraseña no son iguales. Verifique la información e intentelo de nuevo");
            }

            if (usuario.MusuaApprove.Trim().ToLower() != "s")
            {
                throw new ValidationException("La cuenta se encuentra inactiva. Contacte con soporte.");
            }

            usuario.MusuaPassEncrip = new SegMD5().GetMd5Hash(changePassword.Password);
            usuario.MusuaToken = null;
            usuario.MusuaExpirationToken = null;
            
            await _unitOfWorkSEG.SaveChangesAsync();
        }


        /// <summary>
        /// Cambio de password desde móvil
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        public async Task ChangePassword(LoginDTO login)
        {
            var usuario = await _ecmMusuarioRepository.Query(q => q.MusuaEmprId == "VE" && q.MusuaCust == login.UserCust && q.MusuaSuccli == login.UserSuccli).FirstOrDefaultAsync();

            if (usuario == null)
            {
                throw new ValidationException("Usuario no valido");
            }

            if (!string.IsNullOrEmpty(usuario.MusuaPassEncrip) && usuario.MusuaPassEncrip != new SegMD5().GetMd5Hash(login.Password))
            {
                throw new ValidationException("Contraseña o usuario no valido");
            }

            if (usuario.MusuaApprove.Trim().ToLower() != "s")
            {
                throw new ValidationException("La cuenta se encuentra inactiva. Contacte con soporte.");
            }

            usuario.MusuaPassEncrip = new SegMD5().GetMd5Hash(login.NewPassword);
            usuario.MusuaToken = null;
            usuario.MusuaExpirationToken = null;

            await _unitOfWorkSEG.SaveChangesAsync();
        }

        public Task Logout(LoginDTO login)
        {
            throw new NotImplementedException();
        }

        public Task<LoginResultDTO> Refresh(string jwt, string tokenRefresh)
        {
            throw new NotImplementedException();
        }

        public async Task ResetPassword(ResetPasswordDTO resetPassword)
        {
            var usuario = await _ecmMusuarioRepository.Query(q => q.MusuaEmprId == "VE" && q.MusuaCust == resetPassword.UserCust && q.MusuaSuccli == resetPassword.UserSuccli).FirstOrDefaultAsync();

            if (usuario == null)
            {
                throw new ValidationException("Usuario no valido");
            }

            if (usuario.MusuaMail == null || (usuario.MusuaMail.ToLower().Trim() != resetPassword.Email.ToLower().Trim()))
            {
                throw new ValidationException("El correo electrónico no corresponde al registrado para el usuario. Verifique la información.");
            }

            if (usuario.MusuaApprove.Trim().ToLower() != "s")
            {
                throw new ValidationException("La cuenta se encuentra inactiva. Contacte con soporte.");
            }

            var empresa = await _ecmMempresaRepository.Query(q => q.MemprCmpy == usuario.MusuaEmprId).FirstOrDefaultAsync();

            if (empresa == null)
            {
                throw new Exception("Empresa no valida");
            }

            var token = new SegMD5().GetMd5Hash(Guid.NewGuid().ToString());

            var parametros = new { token };

            _logger.LogInformation($"Token {token}");

            await EnviarMensajeTokenReset(token, usuario, empresa);

            usuario.MusuaToken = new SegMD5().GetMd5Hash(token);
            usuario.MusuaExpirationToken = DateTime.Now.AddMinutes(10);

            await _unitOfWorkSEG.SaveChangesAsync();
        }

        private async Task EnviarMensajeTokenReset(string token, EcmMusuario usuario, EcmMempresa empresa)
        {
            var telefono = $"{empresa.MemprePaisId}{usuario.MusuaPhone.Trim()}";

            var parametersMsg = _configuration.GetSection("ResetPasswordMsg").Get<ParametersMsgDTO>();

            var msgSms = parametersMsg.TextSms.Replace("{token}", token);

            await _smsService.Send(empresa.MemprApisms, parametersMsg.FromSms, telefono, msgSms);

            //await _mailService.Send(empresa.MemprApisms, "Recamier S.A. <e-commmerce@mensajeria.recamier.com>", usuario.MusuaMail, "Bienvenido a E-COMMERCE RECAMIER", 143251, new { Usuario = usuario.MusuaName, Correo = usuario.MusuaMail, linkecommercepassword = "" }, generateException: true);

            var link = parametersMsg.Url.Replace("{token}", token);

            await _mailService.Send(empresa.MemprApisms, parametersMsg.FromMail, usuario.MusuaMail, parametersMsg.Subject, parametersMsg.TemplateId, new { Usuario = usuario.MusuaName, Correo = usuario.MusuaMail, linkecommercepassword = link }, generateException: true);
        }

        private string BuildToken(LoginResultDTO loginResult)
        {
            string tokenRefresh = Guid.NewGuid().ToString();

            loginResult.TokenSession = new SegMD5().GetMd5Hash(tokenRefresh);

            var claimsIdentity = GenerateClaimsIdentity(loginResult.User, loginResult.TokenSession);

            return _jwtFactory.GenerateEncodedToken(loginResult.User, claimsIdentity, tokenRefresh);
        }

        public ClaimsIdentity GenerateClaimsIdentity(UserLoginDTO user, string tokenUser)
        {

            var claimsIdentity = new ClaimsIdentity();
            //claimsIdentity.AddClaim(new Claim(JwtRegisteredClaimNames.NameId, JsonConvert.SerializeObject(user.UserId)));
            claimsIdentity.AddClaim(new Claim("CompanyId", user.CompanyId));
            claimsIdentity.AddClaim(new Claim("Cust", user.UserCust));
            claimsIdentity.AddClaim(new Claim("Succli", user.UserSuccli));
            claimsIdentity.AddClaim(new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName));
            claimsIdentity.AddClaim(new Claim(JwtRegisteredClaimNames.Actort, user.FullName ?? $"{user.FirstName} {user.LastName}"));
            claimsIdentity.AddClaim(new Claim(JwtRegisteredClaimNames.Jti, tokenUser));
            claimsIdentity.AddClaim(new Claim(JwtRegisteredClaimNames.Email, user.EmailAddress));
            claimsIdentity.AddClaim(new Claim(JwtRegisteredClaimNames.Birthdate, user.DateOfBirth.ToString("dd/MM/yyyy")));

            return claimsIdentity;
        }

        public async Task<string> UrlApi(string companyId) {
            var empresa = await _ecmMempresaRepository
                           .Query(q => q.MemprCmpy == companyId)
                           .FirstOrDefaultAsync();

            if (empresa == null)
            {
                throw new ValidationException("Empresa no valida, contacte con soporte.");
            }

            return empresa.MemprConn;
        }
    }
}
