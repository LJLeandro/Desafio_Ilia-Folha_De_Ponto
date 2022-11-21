using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Ilia.FolhaDePonto.Api.Data.ValueObjects
{
    public class AlocacaoVO
    {
        public int Id { get; set; }
        public DateTime Dia { get; set; }
        public string Tempo { get; set; }
        public string NomeProjeto { get; set; }
    }
}
