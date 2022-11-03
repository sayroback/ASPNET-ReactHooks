using Microsoft.EntityFrameworkCore;

namespace LeerData
{
  public class AppVentaCursosContext : DbContext
  {
    private const string connectionString = @"Data Source=DESKTOP-JBICHIM\HALLEYSQL;Initial Catalog=CursosOnline;User ID=sa;Password=3242;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      optionsBuilder.UseSqlServer(connectionString);
    }
    public DbSet<Curso> Curso { get; set; }
    public DbSet<Precio> Precio { get; set; }
    public DbSet<Comentario> Comentario { get; set; }
  }
}
