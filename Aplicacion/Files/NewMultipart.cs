using Aplicacion.ManejadorError;
using Dominio.Files;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Persistencia;
using System.Net;

namespace Aplicacion.Files
{
  public class NewMultipart
  {
    public class FormData : IRequest
    {
      public string DirectorioId { get; set; }
      public string Nombre { get; set; }
      public string Segmento { get; set; }
      public string ImageURL { get; set; }
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
        var directorio = await _context.Directorios.FirstOrDefaultAsync(x => x.DirectorioId == request.DirectorioId);
        while (directorio == null)
        {
          var dir = new Directorio
          {
            DirectorioId = request.DirectorioId,
            Nombre = request.Nombre
          };
          _context.Directorios.Add(dir);
          var dirResult = await _context.SaveChangesAsync();
          if (dirResult == 0)
          {
            throw new ManejadorExcepcion(HttpStatusCode.NotFound, new { mensaje = "no se registro directorio" });
          }
          directorio = await _context.Directorios.FirstOrDefaultAsync(x => x.DirectorioId == request.DirectorioId);
        };
        if (directorio != null)
        {
          var formData = new Multipart
          {
            Segmento = request.Segmento,
            ImageURL = request.ImageURL,
            idDirectorio = request.DirectorioId
          };
          _context.Multiparts.Add(formData);
          var valor = await _context.SaveChangesAsync();
          if (valor > 0)
          {
            return Unit.Value;
          }
          throw new ManejadorExcepcion(HttpStatusCode.NotFound, new { mensaje = "no se registro el multipart" });
        }
        throw new ManejadorExcepcion(HttpStatusCode.NotFound, new { mensaje = "no se registro el multipart" });
      }
    }
  }
}
