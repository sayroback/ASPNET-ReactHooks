namespace Dominio.Files
{
  public class Directorio
  {
    public string DirectorioId { get; set; }
    public string Nombre { get; set; }
    public List<Multipart> Multiparts { get; set; }
  }
}
