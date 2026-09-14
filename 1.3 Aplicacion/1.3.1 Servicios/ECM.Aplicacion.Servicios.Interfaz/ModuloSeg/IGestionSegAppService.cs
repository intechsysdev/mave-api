using ECM.Aplicacion.DTO.ModuloSeg;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ECM.Aplicacion.Servicios.Interfaz.ModuloSeg
{
    public interface IGestionSegAppService
    {

        Task<EcmMcontactoDTO> ConsultarContacto(string emprId, int id);

        Task<IEnumerable<EcmMcontactoDTO>> ConsultarContactosEmpresa(string emprId);

        Task<EcmMempresaDTO> ConsultarEmpresa(string id);

        Task<EcmMmonedaDTO> ConsultarMoneda(int id);

        Task<EcmMnappDTO> ConsultarNovedadAplicacion(int nappId, int vappId, int tappId);

        Task<IEnumerable<EcmMnappDTO>> ConsultarNovedadesAplicacion(int vappId, int tappId);

        Task<EcmMtappDTO> ConsultarTipoAplicacion(int id);

        Task<EcmMusuarioDTO> ConsultarUsuario(string cust, string succli);

        Task<EcmMvappDTO> ConsultarVersionAplicacion(int appid, int tappId);

        Task<EcmPaisDTO> ConsultarPais(int id);

        Task RegistrarAcceso(EcmRaccesoDTO item);

        Task<EcmRmacDTO> ConsultarMac(string emprId, string cust, string succli, string mac);

        Task RegistrarMac(EcmRmacDTO item);

        Task<EcmRmacDTO> ActualizarMac(EcmRmacDTO item);

        Task RegistrarLogUsuario(EcmRlogusuarioDTO item);

        Task RegistrarAccesoApp(EcmRnappDTO item);
        Task RegistrarPassword(EcmRpasswordDTO item);

        Task RegistrarTerminosUso(EcmRterminosusoDTO item);
    }
}
