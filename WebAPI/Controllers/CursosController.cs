﻿using Aplicacion.Cursos;
using Dominio;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class CursosController : MiControllerBase
{
  [HttpGet]
  public async Task<ActionResult<List<Curso>>> Get()
  {
    return await Mediator.Send(new Consulta.ListaCursos());
  }
  [HttpGet("{id}")]
  public async Task<ActionResult<Curso>> Detalle(Guid id)
  {
    return await Mediator.Send(new ConsultaId.CursoUnico { Id = id });
  }
  [HttpPost]
  public async Task<ActionResult<Unit>> Crear(Nuevo.Ejecuta data)
  {
    return await Mediator.Send(data);
  }
  //[HttpPut("{id}")]
  //public async Task<ActionResult<Unit>> Editar(Guid id, Editar.Ejecuta data)
  //{
  //  data.CursoId = id;
  //  return await Mediator.Send(data);
  //}
  [HttpDelete("{id}")]
  public async Task<ActionResult<Unit>> Eliminar(Guid id)
  {
    return await Mediator.Send(new Eliminar.Ejecuta { Id = id });
  }
}
