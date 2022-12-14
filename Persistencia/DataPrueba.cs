using Dominio;
using Microsoft.AspNetCore.Identity;

namespace Persistencia;

public class DataPrueba
{
  public static async Task InsertarData(CursosOnlineContext context, UserManager<Usuario> usuarioManager)
  {
    if (!usuarioManager.Users.Any())
    {
      var usuario = new Usuario
      {
        NombreCompleto = "Juan Calderon",
        UserName = "sayroback",
        Email = "sayroback@gmail.com"
      };
      await usuarioManager.CreateAsync(usuario, "Password123?");
    }
  }
}
