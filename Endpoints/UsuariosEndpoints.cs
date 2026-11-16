using Projeto_Final_API_Usuarios.Dtos;

namespace Projeto_Final_API_Usuarios.Endpoints;

public static class UsuariosEndpoints
{
    public static RouteGroupBuilder MapUsuarios(this IEndpointRouteBuilder app)
    {
        var usuarios = app.MapGroup("/api/v1/usuarios");

        usuarios.MapGet("/", () => ListarUsuarios());

        usuarios.MapGet("/{id:guid}", (Guid id) => ConsultarUsuarioPorId(id));

        usuarios.MapPost("/", (EntradaUsuarioDto usuario) => CadastrarUsuario(usuario));

        usuarios.MapPut("/{id:guid}", (Guid id, EntradaUsuarioDto usuario) => AtualizarUsuario(id, usuario));

        usuarios.MapPatch("/{id:guid}/desativacao", (Guid id) => DesativarUsuario(id));
        
        return usuarios;
    }

    private static object DesativarUsuario(Guid id)
    {
        throw new NotImplementedException();
    }

    private static object AtualizarUsuario(Guid id, EntradaUsuarioDto usuario)
    {
        throw new NotImplementedException();
    }

    private static object CadastrarUsuario(EntradaUsuarioDto usuario)
    {
        throw new NotImplementedException();
    }

    private static object ConsultarUsuarioPorId(Guid id)
    {
        throw new NotImplementedException();
    }

    private static object ListarUsuarios()
    {
        throw new NotImplementedException();
    }
}