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

        usuarios.MapGet("/", ([FromServices] UsuarioService usuarioService) =>
            ListarUsuarios(usuarioService)
        );

        usuarios.MapGet("/{id:guid}", ([FromServices] UsuarioService usuarioService, Guid id) =>
            ConsultaUsuarioPorId(usuarioService, id)
        );

        usuarios.MapPost(
            "/",
            ([FromServices] UsuarioService usuarioService, EntradaUsuarioDto entrada) =>
                CadastrarUsuario(usuarioService, entrada)
        );

        usuarios.MapPut(
            "/{id:guid}",
            (
                [FromServices] UsuarioService usuarioService,
                Guid id,
                EntradaAlterarUsuarioDto entrada
            ) => AtualizarUsuario(usuarioService, id, entrada)
        );

        usuarios.MapPatch(
            "/{id:guid}/desativacao",
            ([FromServices] UsuarioService usuarioService, Guid id) =>
                DesativarUsuarioPorId(usuarioService, id)
        );

        return usuarios;
    }

    private static object DesativarUsuarioPorId(UsuarioService usuarioService, Guid id)
    {
        var response = usuarioService.PatchDesativarUsuarioPorId(id);

        switch (response.Item1)
        {
            case (int)HttpStatusCode.NotFound:
                return Results.NotFound(response.Item2);
            case (int)HttpStatusCode.OK:
                return Results.Ok(response.Item2);
            default:
                return Results.InternalServerError();
        }
    }

    private static object AtualizarUsuario(
        UsuarioService usuarioService,
        Guid id,
        EntradaAlterarUsuarioDto entrada
    )
    {
        var response = usuarioService.PutAlterarUsuarioPorId(id, entrada);

        switch (response.Item1)
        {
            case (int)HttpStatusCode.BadRequest:
                return Results.BadRequest(response.Item2);
            case (int)HttpStatusCode.NotFound:
                return Results.NotFound(response.Item2);
            case (int)HttpStatusCode.Conflict:
                return Results.Conflict(response.Item2);
            case (int)HttpStatusCode.OK:
                return Results.Ok(response.Item2);
            default:
                return Results.InternalServerError();
        }
    }

    private static object CadastrarUsuario(UsuarioService usuarioService, EntradaUsuarioDto entrada)
    {
        var response = usuarioService.PostUsuario(entrada);

        switch (response.Item1)
        {
            case (int)HttpStatusCode.BadRequest:
                return Results.BadRequest(response.Item2);
            case (int)HttpStatusCode.Conflict:
                return Results.Conflict(response.Item2);
            case (int)HttpStatusCode.OK:
                return Results.Created("/", response.Item2);
            default:
                return Results.InternalServerError();
        }
    }

    private static object ListarUsuarios(UsuarioService usuarioService)
    {
        var response = usuarioService.GetUsuarios();

        switch (response.Item1)
        {
            case (int)HttpStatusCode.NoContent:
                return Results.NoContent();
            case (int)HttpStatusCode.OK:
                return Results.Ok(response.Item2);
            default:
                return Results.InternalServerError();
        }
    }

    private static object ConsultaUsuarioPorId(UsuarioService usuarioService, Guid id)
    {
        var response = usuarioService.GetUsuarioPorId(id);

        switch (response.Item1)
        {
            case (int)HttpStatusCode.NotFound:
                return Results.NotFound(response.Item2);
            case (int)HttpStatusCode.OK:
                return Results.Ok(response.Item2);
            default:
                return Results.InternalServerError();
        }
    }
}
