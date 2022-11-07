﻿using Aplicacion.ManejadorError;
using Dominio;
using MediatR;
using Persistencia;
using System.Net;

namespace Aplicacion.Cursos;

public class ConsultaId
{
  public class CursoUnico : IRequest<Curso>
  {
    public int Id { get; set; }
  }
  public class Manejador : IRequestHandler<CursoUnico, Curso>
  {
    private readonly CursosOnlineContext _context;
    public Manejador(CursosOnlineContext context)
    {
      _context = context;

    }
    public async Task<Curso> Handle(CursoUnico request, CancellationToken cancellationToken)
    {
      var curso = await _context.Curso.FindAsync(request.Id);
      if (curso == null)
      {
        // throw new Exception("No se encontró el curso");
        throw new ManejadorExcepcion(HttpStatusCode.NotFound, new { mensaje = "No se encontró el curso" });
      }
      return curso;
    }
  }
}
