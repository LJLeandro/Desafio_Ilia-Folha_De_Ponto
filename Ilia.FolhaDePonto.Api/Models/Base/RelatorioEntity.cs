using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ilia.FolhaDePonto.Api.Models.Base
{
    [Table("tb_relatorios")]
    public class RelatorioEntity
    {
        [Key]
        [Column("mes")]
        public string Mes { get; set; }

        [Column("horas_trabalhadas")]
        public string HorasTrabalhadas { get; set; }

        [Column("horas_devidas")]
        public string HorasDevidas { get; set; }

        [Column("horas_excedentes")]
        public string HorasExcedentes { get; set; }
    }
}
