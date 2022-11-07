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
  public DbSet<Directorio> Directorios { get; set; }
  public DbSet<Multipart> Multiparts { get; set; }
  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    modelBuilder.Entity<CursoInstructor>().HasKey(c1 => new
    {
      c1.CursoId,
      c1.InstructorId
    });
    modelBuilder.Entity<Multipart>()
    .HasOne<Directorio>(s => s.Directorio)
    .WithMany(g => g.Multiparts)
    .HasForeignKey(s => s.CurrentDirectorioId);

    modelBuilder.Entity<Directorio>()
       .HasMany<Multipart>(g => g.Multiparts)
       .WithOne(s => s.Directorio)
       .HasForeignKey(s => s.CurrentDirectorioId); ;
  }

  //private const string connectionString = @"";
  //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
  //{
  //  optionsBuilder.UseSqlServer(connectionString);
  //}

}

