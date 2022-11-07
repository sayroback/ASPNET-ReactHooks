﻿using Aplicacion.ManejadorError;
using MediatR;
using Persistencia;
using System.Net;

namespace Aplicacion.Cursos
{
  public class Editar
  {
    public class Ejecuta : IRequest
    {
      public int CursoId { get; set; }
      public string Titulo { get; set; }
      public string Descripcion { get; set; }
      public DateTime? FechaPublicacion { get; set; }
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
        var curso = await _context.Curso.FindAsync(request.CursoId);
        if (curso == null)
        {
          // throw new Exception("No se encontró el curso");
          throw new ManejadorExcepcion(HttpStatusCode.NotFound, new { mensaje = "No se encontró el curso" });
        }
        curso.Titulo = request.Titulo ?? curso.Titulo;
        curso.Descripcion = request.Descripcion ?? curso.Descripcion;
        curso.FechaPublicacion = request.FechaPublicacion ?? curso.FechaPublicacion;

        var result = await _context.SaveChangesAsync();
        if (result > 0)
        {
          return Unit.Value;
        }
        throw new Exception("No se guardaron los cambios en el curso");
      }
    }
  }
}
