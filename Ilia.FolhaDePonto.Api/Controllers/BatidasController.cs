using Ilia.FolhaDePonto.Api.Data.ValueObjects;
using Ilia.FolhaDePonto.Api.Repositories;
using Ilia.FolhaDePonto.Api.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Ilia.FolhaDePonto.Api.Controllers
{
    [Route("v1/batidas")]
    [ApiController]
    public class BatidasController : ControllerBase
    {
        private IRegistroRepository _registroRepository;

        public BatidasController(IRegistroRepository registroRepository)
        {
            _registroRepository = registroRepository;
        }

        [HttpPost]
        public async Task<IActionResult> InsereBatidaAsync([FromBody] MomentoVO momentoVO)
        {
            if (!ModelState.IsValid)
            {
                return ControllerUtils.PreparaMensagemErro("Campo obrigatório não informado.", StatusCodes.Status400BadRequest);
            }

            if (await _registroRepository.QuantidadeDeRegistroPorDia(momentoVO.DataHora.ToString("yyyy-MM-dd")) >= 4)
            {
                return ControllerUtils.PreparaMensagemErro("Apenas 4 horários podem ser registrados por dia", StatusCodes.Status403Forbidden);
            }

            if (await _registroRepository.QuantidadeDeRegistroPorDia(momentoVO.DataHora.ToString("yyyy-MM-dd")) == 2 
                && !await ValidaSeFoiFeitaUmaHoraDeAlmoço(momentoVO))
            {
                return ControllerUtils.PreparaMensagemErro("Deve haver no mínimo 1 hora de almoço", StatusCodes.Status403Forbidden);
            }

            if (ValidaSabadoEDomingo(momentoVO))
            {
                return ControllerUtils.PreparaMensagemErro("Sábado e domingo não são permitidos como dia de trabalho", StatusCodes.Status403Forbidden);
            }

            if (await _registroRepository.VerificaRegistroDuplicado(momentoVO))
            {
                return ControllerUtils.PreparaMensagemErro("Horários já registrado", StatusCodes.Status409Conflict);
            }

            RegistroVO registro = await _registroRepository.SalvarNovoRegistro(momentoVO);

            return new ObjectResult(registro) { StatusCode = StatusCodes.Status201Created };
        }

        private bool ValidaSabadoEDomingo(MomentoVO momentoVO)
        {
            if (momentoVO.DataHora.DayOfWeek == DayOfWeek.Sunday || momentoVO.DataHora.DayOfWeek == DayOfWeek.Saturday)
            {
                return true;
            }

            return false;
        }

        private async Task<bool> ValidaSeFoiFeitaUmaHoraDeAlmoço(MomentoVO momentoVO)
        {
            var registro = await _registroRepository.RecuperaTodosOsRegistrosPorDiaAsync(momentoVO.DataHora.ToString("yyyy-MM-dd"));

            registro.Horarios.Add(momentoVO.DataHora.ToString("HH:mm:ss"));

            registro.Horarios = registro.Horarios.OrderBy(x => x).ToList();

            var minutosDeAlmoço = (DateTime.Parse(registro.Horarios[2]) - DateTime.Parse(registro.Horarios[1])).TotalMinutes;

            if (minutosDeAlmoço >= 60)
                return true;

            return false;
        }

    }
}
