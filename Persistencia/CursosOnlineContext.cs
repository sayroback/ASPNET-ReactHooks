using Dominio;
using Dominio.Files;
using Microsoft.EntityFrameworkCore;

namespace Persistencia;

public class CursosOnlineContext : DbContext
{
  public CursosOnlineContext(DbContextOptions options) : base(options)
  {

  }
  public DbSet<Curso> Curso { get; set; }
  public DbSet<Precio> Precio { get; set; }
  public DbSet<Comentario> Comentario { get; set; }
  public DbSet<Instructor> Instructor { get; set; }
  public DbSet<CursoInstructor> CursoInstructor { get; set; }
  // tablas para los archivos.
  public DbSet<Directorio> Directorios { get; set; }
  public DbSet<Multipart> Multiparts { get; set; }
  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    modelBuilder.Entity<CursoInstructor>().HasKey(c1 => new
    {
      c1.CursoId,
      c1.InstructorId
    });
  }
}

