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
  private readonly IWebHostEnvironment _enviroment;
  public CursosController(IMediator _mediator, IWebHostEnvironment env)
  {
    mediator = _mediator;
    _enviroment = env;
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

  [HttpPost]
  [Consumes("multipart/form-data")]
  public async Task<IActionResult> OnPostUploadAsync([FromForm] Nuevo.Ejecuta data)
  {
    long size = data.files.Sum(f => f.Length);

    foreach (var formFile in data.files)
    {
      if (formFile.Length > 0)
      {
        var filePath = Path.Combine(_enviroment.ContentRootPath, "Uploads", formFile.FileName);

        using (var stream = System.IO.File.Create(filePath))
        {
          await formFile.CopyToAsync(stream);
        }
      }
    }
    await mediator.Send(data);
    // Process uploaded files
    // Don't rely on or trust the FileName property without validation.

    return Ok(new { count = data.files, size });
  }

}
