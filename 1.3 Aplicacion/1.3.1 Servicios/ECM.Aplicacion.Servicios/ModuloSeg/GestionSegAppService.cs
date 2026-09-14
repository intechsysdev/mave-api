using ECM.Aplicacion.DTO.ModuloSeg;
using ECM.Aplicacion.Servicios.Interfaz.ModuloSeg;
using ECM.Dominio.ModuloSeg.Entities;
using ECM.Dominio.ModuloSeg.Repositories;
using ECM.Dominio.UnitsOfWork;
using Itdear.Aplicacion.Core;
using Itdear.Infraestructura.Transversal.ContextAccessor;
using Itdear.Infraestructura.Transversal.Exception;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ECM.Aplicacion.Servicios.ModuloSeg
{
    public class GestionSegAppService : BaseAppService, IGestionSegAppService
    {

        #region Constructor

        private readonly IUnitOfWorkSEG _unitOfWork;
        private readonly IEcmMcontactoRepository _ecmMcontactoRepository;
        private readonly IEcmMempresaRepository _ecmMempresaRepository;
        private readonly IEcmMmonedaRepository _ecmMmonedaRepository;
        private readonly IEcmMnappRepository _ecmMnappRepository;
        private readonly IEcmPaisRepository _ecmPaisRepository;
        private readonly IEcmMtappRepository _ecmMtappRepository;
        private readonly IEcmMusuarioRepository _ecmMusuarioRepository;
        private readonly IEcmMvappRepository _ecmMvappRepository;
        private readonly IEcmRaccesoRepository _ecmRaccesoRepository;
        private readonly IEcmRmacRepository _ecmRmacRepository;
        private readonly IEcmRlogusuarioRepository _ecmRlogusuarioRepository;
        private readonly IEcmRnappRepository _ecmRnappRepository;
        private readonly IEcmRpasswordRepository _ecmRpasswordRepository;
        private readonly IEcmRterminosusoRepository _ecmRterminosusoRepository;

        public GestionSegAppService(
            ILoggerFactory loggerFactory,
            IContextAccessor contextAccessor,
            IUnitOfWorkSEG unitOfWork,
            IEcmMcontactoRepository ecmMcontactoRepository,
            IEcmMempresaRepository ecmMempresaRepository,
            IEcmMmonedaRepository ecmMmonedaRepository,
            IEcmMnappRepository ecmMnappRepository,
            IEcmPaisRepository ecmPaisRepository,
            IEcmMtappRepository ecmMtappRepository,
            IEcmMusuarioRepository ecmMusuarioRepository,
            IEcmMvappRepository ecmMvappRepository,
            IEcmRaccesoRepository ecmRaccesoRepository,
            IEcmRmacRepository ecmRmacRepository,
            IEcmRlogusuarioRepository ecmRlogusuarioRepository,
            IEcmRnappRepository ecmRnappRepository,
            IEcmRpasswordRepository ecmRpasswordRepository,
            IEcmRterminosusoRepository ecmRterminosusoRepository
           ) : base(contextAccessor, loggerFactory)
        {
            _unitOfWork = unitOfWork;
            _ecmMcontactoRepository = ecmMcontactoRepository;
            _ecmMempresaRepository = ecmMempresaRepository;
            _ecmMmonedaRepository = ecmMmonedaRepository;
            _ecmMnappRepository = ecmMnappRepository;
            _ecmPaisRepository = ecmPaisRepository;
            _ecmMtappRepository = ecmMtappRepository;
            _ecmMusuarioRepository = ecmMusuarioRepository;
            _ecmMvappRepository = ecmMvappRepository;
            _ecmRaccesoRepository = ecmRaccesoRepository;
            _ecmRmacRepository = ecmRmacRepository;
            _ecmRlogusuarioRepository = ecmRlogusuarioRepository;
            _ecmRnappRepository = ecmRnappRepository;
            _ecmRpasswordRepository = ecmRpasswordRepository;
            _ecmRterminosusoRepository = ecmRterminosusoRepository;
        }

        #endregion

        public async Task<EcmMcontactoDTO> ConsultarContacto(string emprId, int id)
        {
            var result = await _ecmMcontactoRepository
              .Query(q => q.McontEmprId == emprId && q.McontaId == id)
              .FirstOrDefaultAsync();

            return result.ProjectedAs<EcmMcontactoDTO>();
        }

        public async Task<IEnumerable<EcmMcontactoDTO>> ConsultarContactosEmpresa(string emprId)
        {
            var result = await _ecmMcontactoRepository
            .Query(q => q.McontEmprId == emprId)
            .SelectAsync();

            return result.ProjectedAsCollection<EcmMcontactoDTO>();
        }

        public async Task<EcmMempresaDTO> ConsultarEmpresa(string id)
        {
            var result = await _ecmMempresaRepository
           .Query(q => q.MemprId == id)
           .FirstOrDefaultAsync();

            return result.ProjectedAs<EcmMempresaDTO>();
        }

        public async Task<EcmMmonedaDTO> ConsultarMoneda(int id)
        {
            var result = await _ecmMmonedaRepository
              .Query(q => q.MmoneId == id)
              .FirstOrDefaultAsync();

            return result.ProjectedAs<EcmMmonedaDTO>();
        }

        public async Task<EcmMnappDTO> ConsultarNovedadAplicacion(int nappid, int vappId, int tappId)
        {
            var result = await _ecmMnappRepository
                .Query(q => q.MnappNappId == nappid && q.MnappVappId == vappId && q.MnappTappId == tappId)
                .FirstOrDefaultAsync();

            return result.ProjectedAs<EcmMnappDTO>();
        }

        public async Task<IEnumerable<EcmMnappDTO>> ConsultarNovedadesAplicacion(int vappId, int tappId)
        {
            var result = await _ecmMnappRepository
                .Query(q => q.MnappVappId == vappId && q.MnappTappId == tappId)
                .SelectAsync();

            return result.ProjectedAsCollection<EcmMnappDTO>();
        }

        public async Task<EcmPaisDTO> ConsultarPais(int id)
        {
            var result = await _ecmPaisRepository
                .Query(q => q.MPaisId == id)
                .FirstOrDefaultAsync();

            return result.ProjectedAs<EcmPaisDTO>();
        }

        public async Task<EcmMtappDTO> ConsultarTipoAplicacion(int id)
        {
            var result = await _ecmMtappRepository
              .Query(q => q.RtappId == id)
              .FirstOrDefaultAsync();

            return result.ProjectedAs<EcmMtappDTO>();
        }

        public async Task<EcmMusuarioDTO> ConsultarUsuario(string cust, string succli)
        {
            var result = await _ecmMusuarioRepository
               .Query(q => q.MusuaCust == cust && q.MusuaSuccli == succli)
               .FirstOrDefaultAsync();

            return result.ProjectedAs<EcmMusuarioDTO>();
        }

        public async Task<EcmMvappDTO> ConsultarVersionAplicacion(int appid, int tappId)
        {
            var result = await _ecmMvappRepository
               .Query(q => q.RvappId == appid && q.RvappTappId == tappId)
               .FirstOrDefaultAsync();

            return result.ProjectedAs<EcmMvappDTO>();
        }

        public async Task RegistrarAcceso(EcmRaccesoDTO item)
        {
            var registro = new EcmRacceso
            {
                RacceEmprId = item.RacceEmprId,
                RacceUsuaCust = item.RacceUsuaCust,
                RacceUsuaSuccli = item.RacceUsuaSuccli,
                RacceMac = item.RacceMac,
                RacceVappId = item.RacceVappId,
                RacceTappId = item.RacceTappId
            };
            registro.RacceDate = DateTime.Now;
            _ecmRaccesoRepository.Insert(registro);

            _unitOfWork.SaveChanges();
        }

        public async Task<EcmRmacDTO> ConsultarMac(string emprId, string cust, string succli, string mac)
        {
            var result = await _ecmRmacRepository
               .Query(q => q.RmacEmprId == emprId && q.RmacUsuaCust == cust && q.RmacUsuaSuccli == succli && q.RmacMac == mac)
               .FirstOrDefaultAsync();

            return result.ProjectedAs<EcmRmacDTO>();
        }

        public async Task RegistrarMac(EcmRmacDTO item)
        {
            var registro = new EcmRmac
            {
                RmacEmprId = item.RmacEmprId,
                RmacUsuaCust = item.RmacUsuaCust,
                RmacUsuaSuccli = item.RmacUsuaSuccli,
                RmacMac = item.RmacMac,
                RmacDate = item.RmacDate,                
                RmacVapp2fa = item.RmacVapp2fa
            };

            _ecmRmacRepository.Insert(registro);

            _unitOfWork.SaveChanges();
        }

        public async Task<EcmRmacDTO> ActualizarMac(EcmRmacDTO item)
        {
            var registro = await _ecmRmacRepository
              .Query(q => q.RmacEmprId == item.RmacEmprId && q.RmacUsuaCust == item.RmacUsuaCust && q.RmacUsuaSuccli == item.RmacUsuaSuccli && q.RmacMac == item.RmacMac)
              .FirstOrDefaultAsync();

            if (registro == null) {
                throw new ValidationException("No existe el registro que desea actualizar");
            }


            registro.RmacVapp2fa = item.RmacVapp2fa;
            registro.RmacDate = item.RmacDate;

            _ecmRmacRepository.Update(registro);

            await _unitOfWork.SaveChangesAsync();

            return await ConsultarMac(item.RmacEmprId, item.RmacUsuaCust, item.RmacUsuaSuccli, item.RmacMac);
        }


        public async Task RegistrarLogUsuario(EcmRlogusuarioDTO item)
        {
            var registro = new EcmRlogusuario
            {
                RlogussEmprId = item.RlogussEmprId,
                RlogusUsuaCust = item.RlogusUsuaCust,
                RlogusUsuaSuccli = item.RlogusUsuaSuccli,
                RlogusMac = item.RlogusMac,
                RlogusData = item.RlogusData,
                RlogusValue = item.RlogusValue,
                RlogusDescription = item.RlogusDescription
            };
            registro.RlogusDate = DateTime.Now;
            _ecmRlogusuarioRepository.Insert(registro);

            _unitOfWork.SaveChanges();
        }

        public async Task RegistrarAccesoApp(EcmRnappDTO item)
        {
            var registro = new EcmRnapp
            {
                RnappEmprId = item.RnappEmprId,
                RnappUsuaCust = item.RnappUsuaCust,
                RnappUsuaSuccli = item.RnappUsuaSuccli,
                RnappMac = item.RnappMac,
                RnappApprove = item.RnappApprove,
                RnappVappId = item.RnappVappId,
                RnappTappId = item.RnappTappId,
                RnappNappId = item.RnappNappId
            };
            registro.RnappDate = DateTime.Now;
            _ecmRnappRepository.Insert(registro);

            _unitOfWork.SaveChanges();
        }

        public async Task RegistrarPassword(EcmRpasswordDTO item)
        {
            var registro = new EcmRpassword
            {
                RpassEmprId = item.RpassEmprId,
                RpassUsuaCust = item.RpassUsuaCust,
                RpassUsuaSuccli = item.RpassUsuaSuccli,
                RpassPassword = item.RpassPassword,
                RpassCode = item.RpassCode
            };
            registro.RpassDate = DateTime.Now;
            _ecmRpasswordRepository.Insert(registro);

            _unitOfWork.SaveChanges();
        }

        public async Task RegistrarTerminosUso(EcmRterminosusoDTO item)
        {
            var registro = new EcmRterminosuso
            {
                RtermEmprId = item.RtermEmprId,
                RtermUsuaCust = item.RtermUsuaCust,
                RtermUsuaSuccli = item.RtermUsuaSuccli,
                RtermMac = item.RtermMac,
                RtermApprove = item.RtermApprove,
            };
            registro.RtermDate = DateTime.Now;
            _ecmRterminosusoRepository.Insert(registro);

            _unitOfWork.SaveChanges();
        }
    }
}
