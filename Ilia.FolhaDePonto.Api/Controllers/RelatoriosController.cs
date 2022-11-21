using Ilia.FolhaDePonto.Api.Data.ValueObjects;
using Ilia.FolhaDePonto.Api.Repositories;
using Ilia.FolhaDePonto.Api.Utils;
using Microsoft.AspNetCore.Mvc;

namespace Ilia.FolhaDePonto.Api.Controllers
{
    [Route("v1/folhas-de-ponto")]
    [ApiController]
    public class RelatoriosController : ControllerBase
    {
        IRegistroRepository _registroRepository;
        IAlocacaoRepository _alocacaoRepository;

        public RelatoriosController(IAlocacaoRepository alocacaoRepository, 
            IRegistroRepository registroRepository)
        {
            _alocacaoRepository = alocacaoRepository;
            _registroRepository = registroRepository;
        }

        [HttpGet("{mes}")]
        public async Task<IActionResult> GeraRelatorioMensal(string mes)
        {
            var novoRelatorio = await PreparaNovoRelatorio(mes);

            return new ObjectResult(novoRelatorio) { StatusCode = StatusCodes.Status200OK };
        }

        private async Task<RelatorioVO> PreparaNovoRelatorio(string mes)
        {
            var novoRelatorio = new RelatorioVO()
            {
                Mes = mes
            };

            novoRelatorio.Alocacoes = await _alocacaoRepository.BuscaAlocacoesPorMes(mes);
            novoRelatorio.Registros = await _registroRepository.BuscaRegistroPorMes(mes);

            novoRelatorio.HorasTrabalhadas = FolhaDePontoUtils.CalculaHorasTrabalhadasNoMes(novoRelatorio.Registros).Replace("-", "");
            novoRelatorio.HorasExcedentes = FolhaDePontoUtils.CalculaHorasExcedentesNoMes(novoRelatorio.Alocacoes, novoRelatorio.Registros).Replace("-", "");
            novoRelatorio.HorasDevidas = FolhaDePontoUtils.CalculaHorasDevidasNoMes(novoRelatorio.Alocacoes, novoRelatorio.Registros).Replace("-", "");

            return novoRelatorio;
        }
    }
}
