using AutoMapper;
using ECM.Aplicacion.DTO.ModuloApp;
using ECM.Dominio.ModuloSeg.Entities;

namespace ECM.Aplicacion.DTO.Profiles
{
    public class ModuloAppProfile : Profile
    {
        public ModuloAppProfile()
        {
            AllowNullDestinationValues = true;

            var novedadAppMappingExpression = CreateMap<NovedadAppDTO, EcmMnapp>().ReverseMap();
            novedadAppMappingExpression.ForMember(dto => dto.IdNovedad, (map) => map.MapFrom(o => o.MnappNappId));
            novedadAppMappingExpression.ForMember(dto => dto.DescNovedad, (map) => map.MapFrom(o => o.MnappDescription));
            novedadAppMappingExpression.ForMember(dto => dto.RutaImagen, (map) => map.MapFrom(o => o.MnappImage));
            novedadAppMappingExpression.ForMember(dto => dto.Fecha, (map) => map.MapFrom(o => o.MnappDate));

            var contactoEmpresaMappingExpression = CreateMap<ContactoEmpresaDTO, EcmMcontacto>().ReverseMap();
            contactoEmpresaMappingExpression.ForMember(dto => dto.Nombre, (map) => map.MapFrom(o => o.McontName));
            contactoEmpresaMappingExpression.ForMember(dto => dto.Correo, (map) => map.MapFrom(o => o.McontMail));
            contactoEmpresaMappingExpression.ForMember(dto => dto.Telefono, (map) => map.MapFrom(o => o.McontPhon));
            contactoEmpresaMappingExpression.ForMember(dto => dto.Identificacion, (map) => map.MapFrom(o => o.McontIden));
        }
    }
}
