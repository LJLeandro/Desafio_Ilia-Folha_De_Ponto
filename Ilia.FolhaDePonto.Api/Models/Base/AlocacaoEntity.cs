using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ilia.FolhaDePonto.Api.Models.Base
{
    [Table("tb_alocacoes")]
    public class AlocacaoEntity : BaseEntity
    {
        [Required]
        [Column("dia")]
        
        public DateTime Dia { get; set; }

        [Required]
        [Column("tempo")]
        public string Tempo { get; set; }

        [Required]
        [Column("nome_projeto")]
        public string NomeProjeto { get; set; }
    }
}
