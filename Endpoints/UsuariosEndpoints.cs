using System.Net;
using Microsoft.AspNetCore.Mvc;
using Projeto_Final_API_Usuarios.Dtos;
using Projeto_Final_API_Usuarios.Services;

namespace Projeto_Final_API_Usuarios.Endpoints;

public static class UsuariosEndpoints
{
    public static RouteGroupBuilder MapUsuarios(this IEndpointRouteBuilder app)
    {
        var usuarios = app.MapGroup("/api/v1/usuarios");

        usuarios.MapGet("/", ([FromServices] UsuarioService usuarioService) => ListarUsuarios(usuarioService));

        usuarios.MapGet("/{id:guid}", ([FromServices] UsuarioService usuarioService, Guid id) => ConsultaUsuarioPorId(usuarioService, id));

        usuarios.MapPost(
            "/",
            ([FromServices] UsuarioService usuarioService, EntradaUsuarioDto entrada) =>
                Results.Created("/", usuarioService.PostUsuario(entrada))
        );

        usuarios.MapPut(
            "/{id:guid}",
            (
                [FromServices] UsuarioService usuarioService,
                Guid id,
                EntradaAlterarUsuarioDto entrada
            ) => Results.Ok(usuarioService.PutAlterarUsuarioPorId(id, entrada))
        );

        usuarios.MapPatch(
            "/{id:guid}/desativacao",
            ([FromServices] UsuarioService usuarioService, Guid id) =>
                Results.Ok(usuarioService.PatchDesativarUsuarioPorId(id))
        );

        return usuarios;
    }

    private static object ListarUsuarios(UsuarioService usuarioService)
    {
        var response = usuarioService.GetUsuarios();
        if (response.Item1 != (int)HttpStatusCode.OK)
            return Results.NoContent();

        return Results.Ok(response.Item2);
    }

    private static object ConsultaUsuarioPorId(UsuarioService usuarioService, Guid id)
    {
        var response = usuarioService.GetUsuarioPorId(id);

        if (response.Item1 == (int)HttpStatusCode.NotFound)
            return Results.NotFound(response.Item2);
        else if (response.Item1 == (int)HttpStatusCode.OK)
            return Results.Ok(response.Item2);
        else
            return Results.InternalServerError();
    }
}
