using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Ilia.FolhaDePonto.Api.Data.ValueObjects
{
    public class RelatorioVO
    {
        public string Mes { get; set; }

        public string HorasTrabalhadas { get; set; }

        public string HorasDevidas { get; set; }

        public string HorasExcedentes { get; set; }

        public List<RegistroVO> Registros { get; set; }

        public List<AlocacaoVO> Alocacoes { get; set; }
    }
}
