using Microsoft.AspNetCore.Mvc;
using Projeto_Final_API_Usuarios.Dtos;
using Projeto_Final_API_Usuarios.Services;

namespace Projeto_Final_API_Usuarios.Endpoints;

public static class UsuariosEndpoints
{
    public static RouteGroupBuilder MapUsuarios(this IEndpointRouteBuilder app)
    {
        var usuarios = app.MapGroup("/api/v1/usuarios");

        usuarios.MapGet("/", ([FromServices] UsuarioService usuarioService) =>
            Results.Ok(usuarioService.GetUsuarios()));

        usuarios.MapGet("/{id:guid}", ([FromServices] UsuarioService usuarioService, Guid id) =>
            Results.Ok(usuarioService.GetUsuarioPorId(id)));

        usuarios.MapPost("/", ([FromServices] UsuarioService usuarioService, EntradaUsuarioDto entrada) =>
            Results.Created("/", usuarioService.PostUsuario(entrada)));

        usuarios.MapPut("/{id:guid}", ([FromServices] UsuarioService usuarioService, Guid id, EntradaAlterarUsuarioDto entrada) =>
            Results.Ok(usuarioService.PutAlterarUsuarioPorId(id, entrada)));

        usuarios.MapPatch("/{id:guid}/desativacao", ([FromServices] UsuarioService usuarioService, Guid id) =>
            Results.Ok(usuarioService.PatchDesativarUsuarioPorId(id)));
        
        return usuarios;
    }
}