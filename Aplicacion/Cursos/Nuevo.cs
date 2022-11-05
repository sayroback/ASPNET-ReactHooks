﻿using Dominio;
using MediatR;
using Microsoft.AspNetCore.Http;
using Persistencia;

namespace Aplicacion.Cursos
{
  public class Nuevo
  {
    public class Ejecuta : IRequest
    {
      public string Titulo { get; set; }
      public string Descripcion { get; set; }
      public List<IFormFile> files { get; set; }
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
        var curso = new Curso
        {
          Titulo = request.Titulo,
          Descripcion = request.Descripcion,
          FechaPublicacion = DateTime.Now,
        };
        _context.Curso.Add(curso);
        var valor = await _context.SaveChangesAsync();
        if (valor > 0)
        {
          return Unit.Value;
        }
        throw new Exception("No se pudo insertar el curso");
      }
    }
  }
}
