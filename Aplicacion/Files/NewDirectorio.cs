using Dominio.Files;
using MediatR;
using Persistencia;

namespace Aplicacion.Files
{
  public class NewDirectorio
  {
    public class DirectorioData : IRequest
    {
      public string IdDirectorio { get; set; }
      public string Nombre { get; set; }
    }
    public class Manejador : IRequestHandler<DirectorioData>
    {
      private readonly CursosOnlineContext _context;
      public Manejador(CursosOnlineContext context)
      {
        _context = context;
      }
      public async Task<Unit> Handle(DirectorioData request, CancellationToken cancellationToken)
      {
        var formData = new Directorio
        {
          DirectorioId = request.IdDirectorio,
          Nombre = request.Nombre,
        };
        _context.Directorios.Add(formData);
        var valor = await _context.SaveChangesAsync();
        if (valor > 0)
        {
          return Unit.Value;
        }
        throw new Exception("No se pudo insertar el directorio");
      }
    }
  }
}
