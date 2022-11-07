using System.ComponentModel.DataAnnotations.Schema;

namespace Dominio.Files
{
  public class Multipart
  {
    public int MultipartId { get; set; }
    public string? Segmento { get; set; }
    public string? ImageURL { get; set; }
    public string? idDirectorio { get; set; }
    [ForeignKey("idDirectorio")]
    public Directorio? Directorio { get; set; }
  }
}
