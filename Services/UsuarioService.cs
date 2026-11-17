using System.Net;
using Projeto_Final_API_Usuarios.Dtos;
using Projeto_Final_API_Usuarios.Models;
using Projeto_Final_API_Usuarios.Repositories;

namespace Projeto_Final_API_Usuarios.Services;

public class UsuarioService
{
    private readonly UsuarioRepository _repository;

    public UsuarioService(UsuarioRepository repository)
    {
        _repository = repository;
        _repository.CarregarDadosMockadosNoDicionario();
    }

    public SaidaUsuarioDto GetUsuarios()
    {
        var usuarios = _repository.UsuariosData.Values;

        var usuariosMapeados = MapearRespostaUsuario(usuarios);

        var reponse = new SaidaUsuarioDto
        {
            DadosResposta = usuariosMapeados,
            TempoDaResposta = "14s"
        };

        return reponse;
    }

    public SaidaUsuarioDto GetUsuarioPorId(Guid id)
    {
        var usuarios = _repository.UsuariosData.Values
                            .Where(u => u.Id.Equals(id));

        var usuariosMapeados = MapearRespostaUsuario(usuarios);

        var reponse = new SaidaUsuarioDto
        {
            DadosResposta = usuariosMapeados,
            TempoDaResposta = "14s"
        };

        return reponse;
    }

    public SaidaUsuarioDto PostUsuario(EntradaUsuarioDto entrada)
    {
        var usuario = new Usuario
        {
            Nome = entrada.Nome!,
            Email = entrada.Email!,
            SenhaHash = "#Senha_" + entrada.Senha,
            Cargo = entrada.Cargo!
        };

        _repository.UsuariosData.TryAdd(usuario.Id, usuario);

        var usuarios = new List<Usuario> { usuario };
        var usuariosMapeados = MapearRespostaUsuario(usuarios);

        var reponse = new SaidaUsuarioDto
        {
            DadosResposta = usuariosMapeados,
            TempoDaResposta = "14s"
        };

        return reponse;
    }
    
    public SaidaUsuarioDto PutAlterarUsuarioPorId(Guid id, EntradaAlterarUsuarioDto entrada)
    {
        var usuario = _repository.UsuariosData.Values
                            .FirstOrDefault(u => u.Id.Equals(id));

        if(usuario == null)
            throw new Exception("Usuário não encontrado");

        AtualizarUsuario(usuario, entrada);

        _repository.UsuariosData.TryAdd(id, usuario);

        var usuarios = new List<Usuario> { usuario };
        var usuariosMapeados = MapearRespostaUsuario(usuarios);

        var reponse = new SaidaUsuarioDto
        {
            DadosResposta = usuariosMapeados,
            TempoDaResposta = "14s"
        };

        return reponse;
    }

    private Usuario AtualizarUsuario(Usuario usuario, EntradaAlterarUsuarioDto entrada)
    {
        bool usuarioNaoMudou = usuario.Nome == entrada.Nome &&
                               usuario.Email == entrada.Email &&
                               usuario.Cargo == entrada.Cargo;

        if (!string.IsNullOrEmpty(entrada.Nome))
            usuario.Nome = entrada.Nome;

        if (!string.IsNullOrEmpty(entrada.Email))
            usuario.Email = entrada.Email;

        if (!string.IsNullOrEmpty(entrada.Cargo))
            usuario.Cargo = entrada.Cargo;

        if(!usuarioNaoMudou)
        {
            usuario.AtualizadoEm = DateTime.UtcNow.ToString("dd-MM-yyyy HH:mm:ss");
        }
            
        return usuario;
    }


    public SaidaUsuarioDto PatchDesativarUsuarioPorId(Guid id)
    {
        var usuario = _repository.UsuariosData.Values
                            .FirstOrDefault(u => u.Id.Equals(id));

        if(usuario == null)
            throw new Exception("Usuário não encontrado");

        usuario.Ativo = false;
        _repository.UsuariosData.TryAdd(usuario.Id, usuario);

        var usuarios = new List<Usuario> { usuario };
        var usuariosMapeados = MapearRespostaUsuario(usuarios);

        var reponse = new SaidaUsuarioDto
        {
            DadosResposta = usuariosMapeados,
            TempoDaResposta = "14s"
        };

        return reponse;
    }   
    
    private static IEnumerable<RespostaUsuarioDto> MapearRespostaUsuario(IEnumerable<Usuario> usuarios)
    {
        return usuarios.Select(u => new RespostaUsuarioDto
        {
            Id = u.Id,
            Nome = u.Nome,
            Email = u.Email,
            Ativo = u.Ativo,
            CriadoEm = u.CriadoEm,
            AtualizadoEm = u.AtualizadoEm ?? null,
            Cargo = u.Cargo
        });
    }
}
