using Microsoft.EntityFrameworkCore;

namespace DevBrecho.Data
{
    public class AppDbContext : DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Models.Bolsa> Bolsas { get; set; }
        public DbSet<Models.Fornecedora> Fornecedoras { get; set; }
        public DbSet<Models.Setor> Setores { get; set; }
        public DbSet<Models.PecaCadastrada> PecasCadastradas { get; set; }


    }
}
