using Aplicacion.ManejadorError;
using MediatR;
using Persistencia;
using System.Net;

namespace Aplicacion.Cursos
{
  public class Eliminar
  {
    public class Ejecuta : IRequest
    {
      public int Id { get; set; }
    }

    public class Manejador : IRequestHandler<Ejecuta>
    {
      private readonly CursosOnlineContext _context;
      public Manejador(CursosOnlineContext context)
      {
        _context = context;
      }
      public async Task<Unit> Handle(Ejecuta request, CancellationToken cancellationToken)
      {
        var curso = await _context.Curso.FindAsync(request.Id);
        if (curso == null)
        {
          // throw new Exception("No se encontró el curso");
          throw new ManejadorExcepcion(HttpStatusCode.NotFound, new { mensaje = "No se encontró el curso" });
        }
        _context.Remove(curso);
        var resultado = await _context.SaveChangesAsync();

        if (resultado > 0)
        {
          return Unit.Value;
        }
        throw new Exception("No se pudo eliminar el curso");
      }
    }
  }
}
