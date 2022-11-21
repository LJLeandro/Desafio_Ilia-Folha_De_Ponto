using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ilia.FolhaDePonto.Api.Models.Base
{
    [Table("tb_registros")]
    public class RegistroEntity : BaseEntity
    {
        [Required]
        [Column("dia")]
        public string Dia { get; set; }

        [Column("horarios")]
        public string Horarios { get; set; }
    }
}
