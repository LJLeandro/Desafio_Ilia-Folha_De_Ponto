using AutoMapper;
using Ilia.FolhaDePonto.Api.Data.ValueObjects;
using Ilia.FolhaDePonto.Api.Models.Base;
using Ilia.FolhaDePonto.Api.Models.Context;
using Ilia.FolhaDePonto.Api.Utils;
using Microsoft.EntityFrameworkCore;

namespace Ilia.FolhaDePonto.Api.Repositories
{
    public class AlocacaoRepository : IAlocacaoRepository
    {
        private AppDbContext _context;
        private IMapper _mapper;

        public AlocacaoRepository(IMapper mapper, AppDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<List<AlocacaoVO>> BuscaAlocacoesPorMes(string mesAno)
        {
            var ano = int.Parse(mesAno.Split('-')[0]);
            var mes = int.Parse(mesAno.Split('-')[1]);

            var alocacoesEntity = await (from alocacoes in _context.Alocacoes
                                         where alocacoes.Dia.Year == ano &&
                                            alocacoes.Dia.Month == mes
                                         select alocacoes).ToListAsync();

            List<AlocacaoVO> alocacoesVO = new();

            foreach (var alocacao in alocacoesEntity)
                alocacoesVO.Add(MapUtils.AlocacaoEntityParaAlocacaoVO(alocacao));

            return alocacoesVO;
        }

        public async Task<AlocacaoVO> SalvarAlocacaoAsync(AlocacaoVO alocacaoVO)
        {
            AlocacaoEntity alocacaoEntity = _mapper.Map<AlocacaoEntity>(alocacaoVO);

            _context.Alocacoes.Add(alocacaoEntity);

            await _context.SaveChangesAsync();

            var avo = _mapper.Map<AlocacaoVO>(alocacaoEntity);

            return avo;
        }
    }
}
