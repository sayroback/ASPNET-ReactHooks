using Dominio;

namespace Aplicacion.Contratos
{
    public interface IJwtGenerador
    {
        string GenerateToken(Usuario usuario);
    }
}
