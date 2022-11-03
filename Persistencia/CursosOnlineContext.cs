using Dominio;
using Microsoft.EntityFrameworkCore;

namespace Persistencia;

public class CursosOnlineContext : DbContext
{
  public CursosOnlineContext(DbContextOptions options) : base(options)
  {

  }
  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    modelBuilder.Entity<CursoInstructor>().HasKey(c1 => new
    {
      c1.CursoId,
      c1.InstructorId
    });
  }
  public DbSet<Curso> Curso { get; set; }
  public DbSet<Precio> Precio { get; set; }
  public DbSet<Comentario> Comentario { get; set; }
  public DbSet<Instructor> Instructor { get; set; }
  public DbSet<CursoInstructor> CursoInstructor { get; set; }

  //private const string connectionString = @"";
  //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
  //{
  //  optionsBuilder.UseSqlServer(connectionString);
  //}

}

