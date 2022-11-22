using Aplicacion.Contratos;
using Dominio;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


namespace Seguridad.TokenSeguridad;
public class JwtGenerador : IJwtGenerador
{
  public string CrearToken(Usuario usuario)
  {
    var claims = new List<Claim>
    {
      new Claim(JwtRegisteredClaimNames.NameId, usuario.UserName)
    };

    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("Palabra Secreta 3242 zxc"));
    var credenciales = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

    var tokenDescripcion = new SecurityTokenDescriptor
    {
      Subject = new ClaimsIdentity(claims),
      Expires = DateTime.Now.AddDays(10),
      SigningCredentials = credenciales
    };

    var tokenManejador = new JwtSecurityTokenHandler();
    var token = tokenManejador.CreateToken(tokenDescripcion);

    return tokenManejador.WriteToken(token);
  }
}
