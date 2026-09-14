using ECM.Aplicacion.DTO.ModuloApp;
using ECM.Aplicacion.Servicios.Interfaz.ModuloApp;
using ECM.Dominio.ModuloSeg.Entities;
using ECM.Dominio.ModuloSeg.Repositories;
using ECM.Dominio.UnitsOfWork;
using Itdear.Aplicacion.Core;
using Itdear.Infraestructura.Transversal.ContextAccessor;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ECM.Aplicacion.Servicios.ModuloApp
{
    public class ModuloAppAppService : IModuloAppAppService
    {
        private readonly IUnitOfWorkSEG _unitOfWorkSEG;
        private readonly IContextAccessor _contextAccessor;
        private readonly IEcmMnappRepository _ecmMnappRepository;
        private readonly IEcmRnappRepository _ecmRnappRepository;
        private readonly IEcmMcontactoRepository _ecmMcontactoRepository;
        private readonly IEcmRterminosusoRepository _ecmRterminosusoRepository;

        public ModuloAppAppService(
            IUnitOfWorkSEG unitOfWorkSEG,
            IContextAccessor contextAccessor,
            IEcmMnappRepository ecmMnappRepository,
            IEcmRnappRepository ecmRnappRepository,
            IEcmMcontactoRepository ecmMcontactoRepository,
            IEcmRterminosusoRepository ecmRterminosusoRepository
           )
        {
            _unitOfWorkSEG = unitOfWorkSEG;
            _contextAccessor = contextAccessor;
            _ecmMnappRepository = ecmMnappRepository;
            _ecmRnappRepository = ecmRnappRepository;
            _ecmMcontactoRepository = ecmMcontactoRepository;
            _ecmRterminosusoRepository = ecmRterminosusoRepository;
        }

        public async Task<IEnumerable<NovedadAppDTO>> ConsultarNovedades()
        {
            var result = await _ecmMnappRepository
               .Query(q => q.MnappVappId == _contextAccessor.AppVersion && q.MnappTappId == _contextAccessor.AppType)
               .SelectAsync();

            var novedades = result.ProjectedAsCollection<NovedadAppDTO>();

            foreach (var item in novedades)
            {
                var rnovedad = await _ecmRnappRepository.Query(q => q.RnappEmprId == _contextAccessor.CompanyId &&
                   q.RnappUsuaCust == _contextAccessor.UserCust &&
                   q.RnappUsuaSuccli == _contextAccessor.UserSuccli &&
                   q.RnappVappId == _contextAccessor.AppVersion &&
                   q.RnappTappId == _contextAccessor.AppType &&
                   q.RnappNappId == item.IdNovedad && q.RnappApprove == "S").FirstOrDefaultAsync();

                if (rnovedad != null) {
                    item.Aprobada = true;
                }
            }

            return novedades;
        }

        public async Task<IEnumerable<ContactoEmpresaDTO>> ConsultarContactosEmpresa() 
        {
            var result = await _ecmMcontactoRepository
           .Query(q => q.McontEmprId == _contextAccessor.CompanyId)
           .SelectAsync();

            return result.ProjectedAsCollection<ContactoEmpresaDTO>();
        }

        
        public async Task AceptarNovedades(int idNovedad)
        {
            var rnovedad = await _ecmRnappRepository.Query(q => q.RnappEmprId == _contextAccessor.CompanyId &&
                q.RnappUsuaCust == _contextAccessor.UserCust &&
                q.RnappUsuaSuccli == _contextAccessor.UserSuccli &&
                q.RnappVappId == _contextAccessor.AppVersion &&
                q.RnappTappId == _contextAccessor.AppType &&
                q.RnappNappId == idNovedad).FirstOrDefaultAsync();

            if (rnovedad == null)
            {
                var registro = new EcmRnapp
                {
                    RnappEmprId = _contextAccessor.CompanyId,
                    RnappUsuaCust = _contextAccessor.UserCust,
                    RnappUsuaSuccli = _contextAccessor.UserSuccli,
                    RnappMac = _contextAccessor.ClientIP,
                    RnappDate = DateTime.Now,
                    RnappApprove = "S",
                    RnappVappId  = _contextAccessor.AppVersion,
                    RnappTappId = _contextAccessor.AppType,
                    RnappNappId = idNovedad
                };
                
                _ecmRnappRepository.Insert(registro);
            }
            else
            {
                rnovedad.RnappDate = DateTime.Now;
                rnovedad.RnappApprove = "S";
            }
            _unitOfWorkSEG.SaveChanges();
        }

        #region Terminos y condiciones

        public async Task AceptarTerminosYCondiciones()
        {
            var terminos = await _ecmRterminosusoRepository.Query(q => q.RtermEmprId == _contextAccessor.CompanyId &&
                q.RtermUsuaCust == _contextAccessor.UserCust &&
                q.RtermUsuaSuccli == _contextAccessor.UserSuccli &&
                q.RtermMac == _contextAccessor.ClientIP).FirstOrDefaultAsync();

            if (terminos == null)
            {
                var registro = new EcmRterminosuso
                {
                    RtermEmprId = _contextAccessor.CompanyId,
                    RtermUsuaCust = _contextAccessor.UserCust,
                    RtermUsuaSuccli = _contextAccessor.UserSuccli,
                    RtermMac = _contextAccessor.ClientIP,
                    RtermDate = DateTime.Now,
                    RtermApprove = "S",
                };
                _ecmRterminosusoRepository.Insert(registro);
            }
            else
            {
                terminos.RtermDate = DateTime.Now;
                terminos.RtermApprove = "S";
            }
            _unitOfWorkSEG.SaveChanges();
        }


        #endregion

    }
}
