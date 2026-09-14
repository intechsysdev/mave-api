using ECM.Aplicacion.DTO.Cliente;
using ECM.Aplicacion.Servicios.Interfaz.ModuloCliente;
using ECM.Dominio.ModuloMob.Repositories;
using Itdear.Aplicacion.Core;
using Itdear.Aplicacion.Core.Model;
using Itdear.Infraestructura.Transversal.ContextAccessor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECM.Aplicacion.Servicios.ModuloCliente
{
    public class ModuloClienteAppService : IModuloClienteAppService
    {
        private readonly IContextAccessor _contextAccessor;
        private readonly IMobCarteraRepository _modCarteraRepositorio;
        private readonly IMobRsubMenuRepository _mobRsubMenuRepository;
        private readonly IMobRcustUserRepository _mobRcustUserRepository;

        public ModuloClienteAppService(
            IContextAccessor contextAccessor,
            IMobCarteraRepository modCarteraRepositorio,
            IMobRsubMenuRepository mobRsubMenuRepository,
            IMobRcustUserRepository mobRcustUserRepository
           )
        {
            _contextAccessor = contextAccessor;
            _modCarteraRepositorio = modCarteraRepositorio;
            _mobRsubMenuRepository = mobRsubMenuRepository;
            _mobRcustUserRepository = mobRcustUserRepository;

        }

        #region Menu Usuario

        public async Task<IEnumerable<MenuDTO>> ConsultarMenuUsuario()
        {
            var usuario = await _mobRcustUserRepository
                .Query(q => q.MrcuCmpy == _contextAccessor.CompanyId && q.MrcuCust == _contextAccessor.UserCust && q.MrcuSuccli == _contextAccessor.UserSuccli)
                .FirstOrDefaultAsync();

            var result = _mobRsubMenuRepository.MenuPerfil(_contextAccessor.CompanyId, usuario.MrcuCodPer);

            var subMenu = result.ToList();

            var menuPpal = subMenu.GroupBy(g => g.MobRmenuPpal);

            List<MenuDTO> menu = new List<MenuDTO>();

            foreach (var g in menuPpal)
            {
                menu.Add(g.Key.ProjectedAs<MenuDTO>());
            }

            if (menu.Count == 1)
            {
                return menu[0].Options;
            }

            return menu;
        }

        #endregion

        #region Cartera

        public async Task<IEnumerable<CarteraDTO>> ConsultarCarteraCliente(int? mesesCartera)
        {

            if (mesesCartera == null)
            {
                var result = await _modCarteraRepositorio
                  .Query(q => q.CarCmpy == _contextAccessor.CompanyId && q.CarCust == _contextAccessor.UserCust && q.CarSuccli == _contextAccessor.UserSuccli)
                  .SelectAsync();

                return result.ProjectedAsCollection<CarteraDTO>().OrderBy(o => o.CarFecha);
            }
            else
            {
                var fecha = DateTime.Now.Date;

                fecha = fecha.AddMonths(mesesCartera.GetValueOrDefault() * -1);


                var result = await _modCarteraRepositorio
                  .Query(q => q.CarCmpy == _contextAccessor.CompanyId && q.CarCust == _contextAccessor.UserCust && q.CarSuccli == _contextAccessor.UserSuccli && q.CarFechaven >= fecha)
                  .SelectAsync();

                return result.ProjectedAsCollection<CarteraDTO>().OrderBy(o => o.CarFecha);
            }
        }

        public async Task<ConsultaViewModel<CarteraDTO>> ConsultarCarteraCliente(int? mesesCartera, string textoBusqueda, int pagina, int registrosPorPagina, string ordenarPor, bool direccionOrdenamientoAsc)
        {
            if (mesesCartera == null)
            {
                ConsultaViewModel<CarteraDTO> consulta = new ConsultaViewModel<CarteraDTO>();

                var result = await _modCarteraRepositorio
                     .Query(q => q.CarCmpy == _contextAccessor.CompanyId && q.CarCust == _contextAccessor.UserCust && q.CarSuccli == _contextAccessor.UserSuccli && q.CarNumero.ToString().Contains(textoBusqueda))
                     .OrderBy(ordenarPor, direccionOrdenamientoAsc)
                     .SelectPageAsync(pagina, registrosPorPagina);

                consulta.TotalRegistros = result.TotalItems;

                consulta.Items = result.Items.ProjectedAsCollection<CarteraDTO>();

                return consulta;
            }
            else
            {
                var fecha = DateTime.Now.Date;

                fecha = fecha.AddMonths(mesesCartera.GetValueOrDefault() * -1);

                ConsultaViewModel<CarteraDTO> consulta = new ConsultaViewModel<CarteraDTO>();

                var result = await _modCarteraRepositorio
                     .Query(q => q.CarCmpy == _contextAccessor.CompanyId && q.CarCust == _contextAccessor.UserCust && q.CarSuccli == _contextAccessor.UserSuccli && q.CarFechaven >= fecha)
                     .OrderBy(ordenarPor, direccionOrdenamientoAsc)
                     .SelectPageAsync(pagina, registrosPorPagina);

                consulta.TotalRegistros = result.TotalItems;

                consulta.Items = result.Items.ProjectedAsCollection<CarteraDTO>();

                return consulta;
            }


        }

        #endregion

     }
}
