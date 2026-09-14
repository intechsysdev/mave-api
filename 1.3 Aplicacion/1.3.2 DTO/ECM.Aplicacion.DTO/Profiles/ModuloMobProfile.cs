using AutoMapper;
using ECM.Aplicacion.DTO.ModuloMob;
using ECM.Dominio.ModuloMob.Entities;
using System;

namespace ECM.Aplicacion.DTO.Profiles
{
    public class ModuloMobProfile : Profile
    {
        public ModuloMobProfile()
        {
            AllowNullDestinationValues = true;

            var mobCarteraMappingExpression = CreateMap<MobCarteraDTO, MobCartera>().ReverseMap();
            mobCarteraMappingExpression.ForMember(dto => dto.CarDiasven, (map) => map.MapFrom(o => Math.Floor(DateTime.Now.Subtract(o.CarFechaven.GetValueOrDefault()).TotalDays)));

            var mobEventosItemsMappingExpression = CreateMap<MobEventosItemsDTO, MobEventosItems>().ReverseMap();
            var mobPromocionesMappingExpression = CreateMap<MobPromocionesDTO, MobPromociones>().ReverseMap();
            var mobRcustConsecMappingExpression = CreateMap<MobRcustConsecDTO, MobRcustConsec>().ReverseMap();
            var mobCarteraMobRcustUserMappingExpression = CreateMap<MobRcustUserDTO, MobRcustUser>().ReverseMap();
            var mobRlineaMappingExpression = CreateMap<MobRlineaDTO, MobRlinea>().ReverseMap();
            var mobRordHeadCustMappingExpression = CreateMap<MobRordHeadCustDTO, MobRordHeadCust>().ReverseMap();
            var mobRordLineCustMappingExpression = CreateMap<MobRordLineCustDTO, MobRordLineCust>().ReverseMap();
            var mobRperfilesMappingExpression = CreateMap<MobRperfilesDTO, MobRperfiles>().ReverseMap();
            var mobRperfilMenuPpalMappingExpression = CreateMap<MobRperfilMenuPpalDTO, MobRperfilMenuPpal>().ReverseMap();
            var mobRprodCustMappingExpression = CreateMap<MobRprodCustDTO, MobRprodCust>().ReverseMap();
            var mobRproductosMappingExpression = CreateMap<MobRproductosDTO, MobRproductos>().ReverseMap();
            var mobRsubLineasMappingExpression = CreateMap<MobRsubLineasDTO, MobRsubLineas>().ReverseMap();
            var mobRmenuPpalMappingExpression = CreateMap<MobRmenuPpalDTO, MobRmenuPpal>().ReverseMap();
            var mobRsubMenuMappingExpression = CreateMap<MobRsubMenuDTO, MobRsubMenu>().ReverseMap();
            var mobCalendarioMappingExpression = CreateMap<MobCalendarioDTO, MobCalendario>().ReverseMap();
        }
    }
}
