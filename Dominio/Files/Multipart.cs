using System.ComponentModel.DataAnnotations;

namespace Dominio.Files
{
  public class Multipart
  {
    [Key]
    public int Id { get; set; }
    public string Segmento { get; set; }
    public string ImageURL { get; set; }
    public string CurrentDirectorioId { get; set; }
    public Directorio Directorio { get; set; }
  }
}
