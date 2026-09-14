using ECM.Aplicacion.DTO.Authentication;
using System.Threading.Tasks;

namespace ECM.Aplicacion.Servicios.Interfaz.Authentication
{
    public interface IAuthenticationAppService
    {
        Task<LoginResultDTO> Login(LoginDTO login, string code = null);

        Task<LoginResultDTO> ValidationPassword(LoginDTO login);

        Task Logout(LoginDTO login);

        Task<LoginResultDTO> Refresh(string jwt, string tokenRefresh);

        Task ResetPassword(ResetPasswordDTO resetPassword);

        Task ChangePassword(ChangePasswordDTO changePassword);

        /// <summary>
        /// Cambio de password desde móvil
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        Task ChangePassword(LoginDTO login);

        Task AssingPassword(AssingPasswordDTO changePassword);

        Task<string> UrlApi(string companyId);
    }
}
