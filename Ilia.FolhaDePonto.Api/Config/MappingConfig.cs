using AutoMapper;
using Ilia.FolhaDePonto.Api.Data.ValueObjects;
using Ilia.FolhaDePonto.Api.Models.Base;

namespace Ilia.FolhaDePonto.Api.Config
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<AlocacaoVO, AlocacaoEntity>();
                config.CreateMap<AlocacaoEntity, AlocacaoVO>();
            });

            return mappingConfig;
        }
    }
}
