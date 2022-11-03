using Aplicacion.Cursos;
using Dominio;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class CursosController : ControllerBase
{
  private readonly IMediator mediator;
  public CursosController(IMediator _mediator)
  {
    mediator = _mediator;
  }

  [HttpGet]
  public async Task<ActionResult<List<Curso>>> Get()
  {
    return await mediator.Send(new Consulta.ListaCursos());
  }
  [HttpGet("{id}")]
  public async Task<ActionResult<Curso>> Detalle(int id)
  {
    return await mediator.Send(new ConsultaId.CursoUnico { Id = id });
  }
}
