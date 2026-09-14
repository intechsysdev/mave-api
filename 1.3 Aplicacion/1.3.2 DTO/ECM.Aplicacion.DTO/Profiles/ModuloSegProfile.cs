using AutoMapper;
using ECM.Aplicacion.DTO.ModuloSeg;
using ECM.Dominio.ModuloSeg.Entities;

namespace ECM.Aplicacion.DTO.Profiles
{
    public class ModuloSegProfile : Profile
    {
        public ModuloSegProfile()
        {
            AllowNullDestinationValues = true;

            var ecmMcontactoMappingExpression = CreateMap<EcmMcontactoDTO, EcmMcontacto>().ReverseMap();
            var ecmMempresaMappingExpression = CreateMap<EcmMempresaDTO, EcmMempresa>().ReverseMap();
            var ecmMmonedaMappingExpression = CreateMap<EcmMmonedaDTO, EcmMmoneda>().ReverseMap();
            var ecmMnappMappingExpression = CreateMap<EcmMnappDTO, EcmMnapp>().ReverseMap();
            var ecmMtappMappingExpression = CreateMap<EcmMtappDTO, EcmMtapp>().ReverseMap();
            var ecmMusuarioMappingExpression = CreateMap<EcmMusuarioDTO, EcmMusuario>().ReverseMap();
            var ecmMvappMappingExpression = CreateMap<EcmMvappDTO, EcmMvapp>().ReverseMap();
            var ecmPaisMappingExpression = CreateMap<EcmPaisDTO, EcmPais>().ReverseMap();
            var ecmRaccesoMappingExpression = CreateMap<EcmRaccesoDTO, EcmRacceso>().ReverseMap();
            var ecmRlogusuarioMappingExpression = CreateMap<EcmRlogusuarioDTO, EcmRlogusuario>().ReverseMap();
            var ecmRnappMappingExpression = CreateMap<EcmRnappDTO, EcmRnapp>().ReverseMap();
            var ecmRpasswordMappingExpression = CreateMap<EcmRpasswordDTO, EcmRpassword>().ReverseMap();
            var ecmRterminousoMappingExpression = CreateMap<EcmRterminosusoDTO, EcmRterminosuso>().ReverseMap();
        }
    }
}
