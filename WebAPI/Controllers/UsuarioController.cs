using Aplicacion.Seguridad;
using Dominio;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

public class UsuarioController : MiControllerBase
{
  [HttpPost("login")]
  public async Task<ActionResult<Usuario>> Login(Login.Ejecuta parametros)
  {
    return await Mediator.Send(parametros);
  }
}
