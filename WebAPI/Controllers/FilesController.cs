using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class FilesController : ControllerBase
{
  private readonly IMediator mediator;
  private readonly IWebHostEnvironment _enviroment;
  public FilesController(IMediator _mediator, IWebHostEnvironment env)
  {
    mediator = _mediator;
    _enviroment = env;
  }

  [HttpPost]
  public async Task<IActionResult> OnPostUploadAsync(List<IFormFile> files)
  {
    long size = files.Sum(f => f.Length);

    foreach (var formFile in files)
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

    // Process uploaded files
    // Don't rely on or trust the FileName property without validation.

    return Ok(new { count = files.Count, size });
  }
}
