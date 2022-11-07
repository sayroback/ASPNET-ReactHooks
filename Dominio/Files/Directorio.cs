using System.ComponentModel.DataAnnotations;

namespace Dominio.Files
{
  public class Directorio
  {
    [Key]
    public string DirectorioId { get; set; }
    public string Nombre { get; set; }
    public ICollection<Multipart> Multiparts { get; set; }
  }
}
