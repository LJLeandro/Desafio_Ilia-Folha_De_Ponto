using System.ComponentModel.DataAnnotations;

namespace Ilia.FolhaDePonto.Api.Data.ValueObjects
{
    public class MomentoVO
    {
        [Required]
        public DateTime DataHora { get; set; }
    }
}
