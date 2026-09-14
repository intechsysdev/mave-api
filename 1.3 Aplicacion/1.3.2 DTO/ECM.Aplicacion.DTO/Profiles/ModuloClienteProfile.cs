using AutoMapper;
using ECM.Aplicacion.DTO.Cliente;
using ECM.Dominio.ModuloMob.Entities;

namespace ECM.Aplicacion.DTO.Profiles
{
    public class ModuloClienteProfile : Profile
    {
        public ModuloClienteProfile()
        {
            AllowNullDestinationValues = true;

            var carteraMappingExpression = CreateMap<CarteraDTO, MobCartera>().ReverseMap();
            
        }
    }
}
