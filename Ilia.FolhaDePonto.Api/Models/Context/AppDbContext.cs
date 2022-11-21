using Ilia.FolhaDePonto.Api.Models.Base;
using Microsoft.EntityFrameworkCore;

namespace Ilia.FolhaDePonto.Api.Models.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<RegistroEntity> Registros { get; set; }
        public DbSet<AlocacaoEntity> Alocacoes { get; set; }
        public DbSet<RelatorioEntity> Relatorios { get; set; }
    }
}
