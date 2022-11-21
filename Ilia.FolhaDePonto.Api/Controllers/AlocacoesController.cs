using Ilia.FolhaDePonto.Api.Data.ValueObjects;
using Ilia.FolhaDePonto.Api.Repositories;
using Ilia.FolhaDePonto.Api.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Win32;

namespace Ilia.FolhaDePonto.Api.Controllers
{
    [Route("v1/alocacoes")]
    [ApiController]
    public class AlocacoesController : ControllerBase
    {
        IRegistroRepository _registroRepository;
        IAlocacaoRepository _alocacaoRepository;

        public AlocacoesController(IRegistroRepository registroRepository, IAlocacaoRepository alocacaoRepository)
        {
            _registroRepository = registroRepository;
            _alocacaoRepository = alocacaoRepository;
        }

        [HttpPost]
        public async Task<IActionResult> InsereAlocacaoAsync([FromBody] AlocacaoVO alocacaoVO)
        {
            var registroDia = await _registroRepository.RecuperaTodosOsRegistrosPorDiaAsync(alocacaoVO.Dia.ToString("yyyy-MM-dd"));

            if (registroDia == null)
            {
                return ControllerUtils.PreparaMensagemErro("Não pode alocar tempo maior que o tempo trabalhado no dia", StatusCodes.Status400BadRequest) ;
            } 
            else if (registroDia.Horarios.Count == 4)
            {
                string horaFormatada = FolhaDePontoUtils.ExcluiFormatacaoDaHora(alocacaoVO.Tempo);

                var horasAlocadas = TimeSpan.Parse(horaFormatada).TotalMinutes;

                var horasTrabalhas = FolhaDePontoUtils.CalculaTempoTrabalhado(registroDia);

                if (horasAlocadas > horasTrabalhas)
                    return ControllerUtils.PreparaMensagemErro("Não pode alocar tempo maior que o tempo trabalhado no dia", StatusCodes.Status409Conflict);
            }

            var alocacao = await _alocacaoRepository.SalvarAlocacaoAsync(alocacaoVO);

            return new ObjectResult(alocacao) { StatusCode = StatusCodes.Status201Created };
        }
    }
}
