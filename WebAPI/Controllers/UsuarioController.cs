﻿using Aplicacion.Seguridad;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

public class UsuarioController : MiControllerBase
{
    [HttpPost("login")]
    public async Task<ActionResult<UsuarioData>> Login(Login.Ejecuta parametros)
    {
        return await Mediator.Send(parametros);
    }
}
