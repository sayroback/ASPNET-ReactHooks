using Aplicacion.Files;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Persistencia;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class FilesController : ControllerBase
{
  private readonly IMediator _mediator;
  private readonly IWebHostEnvironment _enviroment;
  private readonly CursosOnlineContext _context;
  public FilesController(IMediator mediator, IWebHostEnvironment env, CursosOnlineContext context)
  {
    _context = context;
    _mediator = mediator;
    _enviroment = env;
  }

  [HttpPost]
  public async Task<ActionResult<Unit>> OnPostUploadAsync([FromForm] NewMultipart.FormData data)
  {
    List<string> paths = new List<string>();
    long size = data.ImgSeg.files.Sum(f => f.Length);
    int index = 0;
    foreach (var formFile in data.ImgSeg.files)
    {
      if (formFile.Length > 0)
      {
        string nombre = String.Format("{1:yyyyMMdd_hhmmssfff}{2}", Path.GetFileNameWithoutExtension(formFile.FileName), DateTime.Now, Path.GetExtension(formFile.FileName));
        string pathLocal = "~/Uploads";
        Directory.CreateDirectory(pathLocal);

        Stream fs = formFile.OpenReadStream();
        BinaryReader br = new BinaryReader(fs);
        byte[] bytes = br.ReadBytes((Int32)fs.Length);


        var filePath = Path.Combine(_enviroment.ContentRootPath, "Uploads", nombre);
        using (var stream = new System.IO.FileStream(filePath, FileMode.Create))
        {
          await formFile.CopyToAsync(stream);
        }



        System.IO.File.Delete(filePath);
        string directoryName = Path.GetFullPath(filePath);
        paths.Add(directoryName);
        data.ImageURL = directoryName;
        data.indexImg = index;
        await _mediator.Send(data);
      }
      index++;
    }

    return Ok();
  }
}
