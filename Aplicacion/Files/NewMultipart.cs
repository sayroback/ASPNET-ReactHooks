using Dominio;
using MediatR;
using Microsoft.AspNetCore.Http;
using Persistencia;


namespace Aplicacion.Files
{
  public class NewMultipart
  {
    public class FormData : IRequest
    {
      public string idDirectorio { get; set; }
      public string Nombre { get; set; }
      public string Segmento { get; set; }
      public string? ImageURL { get; set; }
      public List<IFormFile> files { get; set; }
    }
    public class Manejador : IRequestHandler<FormData>
    {
      private readonly CursosOnlineContext _context;
      public Manejador(CursosOnlineContext context)
      {
        _context = context;
      }
      public async Task<Unit> Handle(FormData request, CancellationToken cancellationToken)
      {
        var dir = new Directorio
        {
          DirectorioId = request.idDirectorio,
          Nombre = request.Nombre
        };
        _context.Directorios.Add(dir);
        var dirResult = await _context.SaveChangesAsync();

        if (dirResult > 0)
        {
          var formData = new Multipart
          {
            Segmento = request.Segmento,
            ImageURL = request.ImageURL,
            CurrentDirectorioId = request.idDirectorio
          };
          _context.Multiparts.Add(formData);
          var valor = await _context.SaveChangesAsync();
          if (valor > 0)
          {
            return Unit.Value;
          }
        }
        throw new Exception("No se pudo insertar el curso");
      }
    }
  }
}
