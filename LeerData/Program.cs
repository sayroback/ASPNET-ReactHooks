using Microsoft.EntityFrameworkCore;

namespace LeerData;
class Program
{
  private static void Main(string[] args)
  {
    using (var db = new AppVentaCursosContext())
    {
      var cursos = db.Curso.Include(c => c.InstructorLink).ThenInclude(c1 => c1.Instructor).AsNoTracking();

      foreach (var curso in cursos)
      {
        Console.WriteLine(curso.Titulo);
        foreach (var insLink in curso.InstructorLink)
        {
          Console.WriteLine("---- " + insLink.Instructor.Nombre + " " + insLink.Instructor.Apellido);
        }
      }

      //var cursos = db.Curso.Include(c => c.ComentarioLista).AsNoTracking();
      //foreach (var curso in cursos)
      //{
      //  Console.WriteLine(curso.Titulo);
      //  foreach (var comentario in curso.ComentarioLista)
      //  {
      //    Console.WriteLine("*****  " + comentario.ComentarioTexto);
      //  }
      //}

      //var cursos = db.Curso.Include(p => p.PrecioPromocion).AsNoTracking();
      //foreach (var curso in cursos)
      //{
      //  //Console.WriteLine(curso.Titulo + " --- " + curso.Descripcion);
      //  Console.WriteLine(curso.Titulo + " --- " + curso.PrecioPromocion.PrecioActual);
      //}
    }
  }
}