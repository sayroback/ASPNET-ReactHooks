using Aplicacion.Files;
using Dominio;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Persistencia;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class FilesController : ControllerBase
{
  private readonly IMediator mediator;
  private readonly IWebHostEnvironment _enviroment;
  private readonly CursosOnlineContext _context;
  public FilesController(IMediator _mediator, IWebHostEnvironment env, CursosOnlineContext context)
  {
    _context = context;
    mediator = _mediator;
    _enviroment = env;
  }

  [HttpPost]
  public async Task<ActionResult<Unit>> OnPostUploadAsync([FromForm] NewMultipart.FormData data)
  {
    var formData = new Directorio
    {
      DirectorioId = data.idDirectorio,
      Nombre = data.Nombre,
    };
    _context.Directorios.Add(formData);
    var valor = await _context.SaveChangesAsync();
    if (valor > 0)
    {
      List<string> paths = new List<string>();
      long size = data.files.Sum(f => f.Length);
      foreach (var formFile in data.files)
      {
        if (formFile.Length > 0)
        {
          string nombre = String.Format("{1:yyyyMMdd_hhmmssfff}{2}", Path.GetFileNameWithoutExtension(formFile.FileName), DateTime.Now, Path.GetExtension(formFile.FileName));
          var filePath = Path.Combine(_enviroment.ContentRootPath, "Uploads", nombre);
          using (var stream = System.IO.File.Create(filePath))
          {
            await formFile.CopyToAsync(stream);
          }
          string directoryName = Path.GetFullPath(filePath);
          paths.Add(directoryName);
          data.ImageURL = directoryName;
          await mediator.Send(data);
        }

      }
      return Ok(new { path = paths });
    }
    return BadRequest();
  }
}
