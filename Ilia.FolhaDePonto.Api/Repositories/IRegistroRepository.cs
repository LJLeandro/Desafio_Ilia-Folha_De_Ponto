using Ilia.FolhaDePonto.Api.Data.ValueObjects;

namespace Ilia.FolhaDePonto.Api.Repositories
{
    public interface IRegistroRepository
    {
        Task<RegistroVO> RecuperaTodosOsRegistrosPorDiaAsync(string dia);
        Task<int> QuantidadeDeRegistroPorDia(string dia);
        Task<bool> VerificaRegistroDuplicado(MomentoVO momentoVO);
        Task<RegistroVO> SalvarNovoRegistro(MomentoVO momentoVO);
        Task<RegistroVO> AtualizarRegistroAsync(MomentoVO momentoVO);
        Task<List<RegistroVO>> BuscaRegistroPorMes(string mes);
    }
}
