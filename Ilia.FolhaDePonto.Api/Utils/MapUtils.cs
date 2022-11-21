using AutoMapper;
using Ilia.FolhaDePonto.Api.Data.ValueObjects;
using Ilia.FolhaDePonto.Api.Models.Base;

namespace Ilia.FolhaDePonto.Api.Utils
{
    public static class MapUtils
    {
        public static RelatorioVO RelatorioEntityParaRelatorioVO(RelatorioEntity relatorioEntity)
        {
            return new RelatorioVO()
            {
                Mes = relatorioEntity.Mes,
                HorasDevidas = relatorioEntity.HorasDevidas,
                HorasExcedentes = relatorioEntity.HorasExcedentes,
                HorasTrabalhadas = relatorioEntity.HorasTrabalhadas
            };
        }

        public static RelatorioEntity RelatorioVOParaRelatorioEntity(RelatorioVO relatorioVO)
        {
            return new RelatorioEntity()
            {
                Mes = relatorioVO.Mes,
                HorasDevidas = relatorioVO.HorasDevidas,
                HorasExcedentes = relatorioVO.HorasExcedentes,
                HorasTrabalhadas = relatorioVO.HorasTrabalhadas
            };
        }

        public static AlocacaoVO AlocacaoEntityParaAlocacaoVO(AlocacaoEntity alocacaoEntity)
        {
            return new AlocacaoVO()
            {
                Dia = alocacaoEntity.Dia,
                Id = alocacaoEntity.Id,
                NomeProjeto = alocacaoEntity.NomeProjeto,
                Tempo = alocacaoEntity.Tempo
            };
        }

        public static AlocacaoEntity AlocacaoVOParaAlocacaoEntity(AlocacaoVO alocacaoVO)
        {
            return new AlocacaoEntity()
            {
                Dia = alocacaoVO.Dia,
                Id = alocacaoVO.Id,
                NomeProjeto = alocacaoVO.NomeProjeto,
                Tempo = alocacaoVO.Tempo
            };
        }

        public static RegistroVO RegistroEntityParaRegistroVO(RegistroEntity registroEntity)
        {
            return new RegistroVO()
            {
                Dia = registroEntity.Dia,
                Horarios = registroEntity.Horarios.Split(';').ToList()
            };
        }

        public static RegistroEntity RegistroVOParaRegistroEntity(RegistroVO registroVO)
        {
            return new RegistroEntity()
            {
                Dia = registroVO.Dia,
                Horarios = RegistroUtils.OrdernaHorariosParaString(registroVO.Horarios)
            };
        }


    }
}
