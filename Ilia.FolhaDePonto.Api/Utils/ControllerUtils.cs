using Ilia.FolhaDePonto.Api.Data.ValueObjects;
using Microsoft.AspNetCore.Mvc;

namespace Ilia.FolhaDePonto.Api.Utils
{
    public static class ControllerUtils
    {
        public static IActionResult PreparaMensagemErro(string mensagem, int statusCode)
        {
            return new ObjectResult(new MensagemVO()
            {
                Mensagem = mensagem
            })
            { StatusCode = statusCode };
        }
    }
}
