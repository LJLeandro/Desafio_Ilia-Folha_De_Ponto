using Ilia.FolhaDePonto.Api.Data.ValueObjects;

namespace Ilia.FolhaDePonto.Api.Repositories
{
    public interface IAlocacaoRepository
    {
        Task<List<AlocacaoVO>> BuscaAlocacoesPorMes(string mes);
        Task<AlocacaoVO> SalvarAlocacaoAsync(AlocacaoVO alocacaoVO);
    }
}
