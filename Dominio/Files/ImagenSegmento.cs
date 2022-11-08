using Microsoft.AspNetCore.Http;

namespace Dominio.Files
{
  public class ImagenSegmento
  {
    public List<string> Segmento { get; set; }
    public List<IFormFile> files { get; set; }
  }
}
