using System.Net;
using System.Net.Mail;
using Projeto_Final_API_Usuarios.Constants;
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

    // TODO : Passar os erros de validação para constantes.

    public Tuple<int, SaidaUsuarioDto> GetUsuarios()
    {
        IEnumerable<Usuario> usuarios = _repository.UsuariosData.Values.ToList();

        if (!usuarios.Any())
            return new Tuple<int, SaidaUsuarioDto>((int)HttpStatusCode.NoContent, new());

        var response = MontarOkResponse(usuarios);

        return new Tuple<int, SaidaUsuarioDto>((int)HttpStatusCode.OK, response);
    }

    public Tuple<int, dynamic> GetUsuarioPorId(Guid id)
    {
        var usuarios = _repository.UsuariosData.Values.Where(u => u.Id.Equals(id));

        if (usuarios.Count() == 0)
        {
            var erroReponse = MontarErro(
                ErrosContants.COD_USUARIO_NAO_ENCONTRADO,
                ErrosContants.MSG_USUARIO_NAO_ENCONTRADO
            );
            return new Tuple<int, dynamic>((int)HttpStatusCode.NotFound, erroReponse);
        }

        var response = MontarOkResponse(usuarios);

        return new Tuple<int, dynamic>((int)HttpStatusCode.OK, response);
    }

    public Tuple<int, dynamic> PostUsuario(EntradaUsuarioDto entrada)
    {
        var entradaValida = ValidarEntradaUsuario(entrada);
        if (entradaValida is SaidaErroDto)
        {
            return new Tuple<int, dynamic>((int)HttpStatusCode.BadRequest, entradaValida);
        }

        //TODO: Fazer a validação da existência do e-mail no cadastro, deve ser único.
        var usuariosExistentes = _repository.UsuariosData.Values.ToList();
        var emailExisteNaLista = usuariosExistentes.Any(u => u.Email == entrada.Email);
        if (emailExisteNaLista)
        {
            var erroReponse = MontarErro(
                ErrosContants.COD_EMAIL_CADASTRADO,
                ErrosContants.MSG_EMAIL_CADASTRADO
            );
            return new Tuple<int, dynamic>((int)HttpStatusCode.Conflict, erroReponse);
        }

        var usuario = new Usuario
        {
            Nome = entrada.Nome!,
            Email = entrada.Email!,
            SenhaHash = "#Senha_" + entrada.Senha,
            Cargo = entrada.Cargo!,
        };

        _repository.UsuariosData.TryAdd(usuario.Id, usuario);

        var usuarios = new List<Usuario> { usuario };

        var response = MontarOkResponse(usuarios);

        return new Tuple<int, dynamic>((int)HttpStatusCode.OK, response);
    }

    public Tuple<int, dynamic> PutAlterarUsuarioPorId(Guid id, EntradaAlterarUsuarioDto entrada)
    {
        var entradaValida = ValidarEntradaAlterarUsuario(entrada);
        if (entradaValida is SaidaErroDto)
        {
            return new Tuple<int, dynamic>((int)HttpStatusCode.BadRequest, entradaValida);
        }

        //Validação da existência do e-mail no cadastro, deve ser único.
        var usuariosExistentes = _repository.UsuariosData.Values.ToList();
        var emailExisteNaLista = usuariosExistentes.Any(u => u.Email == entrada.Email);
        if (emailExisteNaLista)
        {
            var erroReponse = MontarErro(
                ErrosContants.COD_EMAIL_CADASTRADO,
                ErrosContants.MSG_EMAIL_CADASTRADO
            );
            return new Tuple<int, dynamic>((int)HttpStatusCode.Conflict, erroReponse);
        }

        var usuario = _repository.UsuariosData.Values.FirstOrDefault(u => u.Id.Equals(id));

        if (usuario == null)
        {
            var erroReponse = MontarErro(
                ErrosContants.COD_USUARIO_NAO_ENCONTRADO,
                ErrosContants.MSG_USUARIO_NAO_ENCONTRADO
            );

            return new Tuple<int, dynamic>((int)HttpStatusCode.NotFound, erroReponse);
        }

        AtualizarUsuario(usuario, entrada);

        _repository.UsuariosData.TryAdd(id, usuario);

        var usuarios = new List<Usuario> { usuario };

        var response = MontarOkResponse(usuarios);

        return new Tuple<int, dynamic>((int)HttpStatusCode.OK, response);
    }

    public Tuple<int, dynamic> PatchDesativarUsuarioPorId(Guid id)
    {
        var usuario = _repository.UsuariosData.Values.FirstOrDefault(u => u.Id.Equals(id));

        if (usuario == null)
        {
            var erroReponse = MontarErro(
                ErrosContants.COD_USUARIO_NAO_ENCONTRADO,
                ErrosContants.MSG_USUARIO_NAO_ENCONTRADO
            );

            return new Tuple<int, dynamic>((int)HttpStatusCode.NotFound, erroReponse);
        }

        usuario.Ativo = false;
        _repository.UsuariosData.TryAdd(usuario.Id, usuario);

        var usuarios = new List<Usuario> { usuario };

        var response = MontarOkResponse(usuarios);

        return new Tuple<int, dynamic>((int)HttpStatusCode.OK, response);
    }

    // Métodos auxiliares privados
    private static IEnumerable<RespostaUsuarioDto> MapearRespostaUsuario(
        IEnumerable<Usuario> usuarios
    )
    {
        return usuarios.Select(u => new RespostaUsuarioDto
        {
            Id = u.Id,
            Nome = u.Nome,
            Email = u.Email,
            Ativo = u.Ativo,
            CriadoEm = u.CriadoEm,
            AtualizadoEm = u.AtualizadoEm ?? null,
            Cargo = u.Cargo,
        });
    }

    private Usuario AtualizarUsuario(Usuario usuario, EntradaAlterarUsuarioDto entrada)
    {
        bool usuarioNaoMudou =
            usuario.Nome == entrada.Nome
            && usuario.Email == entrada.Email
            && usuario.Cargo == entrada.Cargo;

        if (!string.IsNullOrEmpty(entrada.Nome))
            usuario.Nome = entrada.Nome;

        if (!string.IsNullOrEmpty(entrada.Email))
            usuario.Email = entrada.Email;

        if (!string.IsNullOrEmpty(entrada.Cargo))
            usuario.Cargo = entrada.Cargo;

        if (!usuarioNaoMudou)
        {
            usuario.AtualizadoEm = DateTime.UtcNow.ToString(ErrosContants.STRING_DATA_PADRAO);
        }

        return usuario;
    }

    private SaidaUsuarioDto MontarOkResponse(IEnumerable<Usuario> usuarios)
    {
        var usuariosMapeados = MapearRespostaUsuario(usuarios);

        return new SaidaUsuarioDto { DadosResposta = usuariosMapeados, TempoDaResposta = "14s" };
    }

    private SaidaErroDto MontarErro(string _codigo, string _mensagem)
    {
        var listaDeErros = new List<RespostaErroDto>();
        var erro = new RespostaErroDto { Codigo = _codigo, Mensagem = _mensagem };

        listaDeErros.Add(erro);

        var erroReponse = new SaidaErroDto
        {
            DadosResposta = listaDeErros,
            TempoDaResposta = "14s",
        };

        return erroReponse;
    }

    private dynamic ValidarEntradaUsuario(EntradaUsuarioDto entrada)
    {
        var listaDeErros = new List<RespostaErroDto>();

        if (string.IsNullOrWhiteSpace(entrada.Nome) || entrada.Nome.Length < 3)
        {
            var erro = new RespostaErroDto
            {
                Codigo = ErrosContants.COD_NOME_INVALIDO,
                Mensagem = ErrosContants.MSG_NOME_INVALIDO
            };
            listaDeErros.Add(erro);
        }

        if (!MailAddress.TryCreate(entrada.Email, out var endereco) || !endereco.Host.Contains('.'))
        {
            var erro = new RespostaErroDto
            {
                Codigo = ErrosContants.COD_EMAIL_INVALIDO,
                Mensagem = ErrosContants.MSG_EMAIL_INVALIDO
            };
            listaDeErros.Add(erro);
        }

        if (entrada.Senha == null || entrada.Senha.Length < 8)
        {
            var erro = new RespostaErroDto
            {
                Codigo = ErrosContants.COD_SENHA_INVALIDA,
                Mensagem = ErrosContants.MSG_SENHA_INVALIDA
            };
            listaDeErros.Add(erro);
        }

        if (listaDeErros.Count > 0)
        {
            var erroReponse = new SaidaErroDto
            {
                DadosResposta = listaDeErros,
                TempoDaResposta = "14s",
            };

            return erroReponse;
        }

        return true;
    }

    private dynamic ValidarEntradaAlterarUsuario(EntradaAlterarUsuarioDto entrada)
    {
        var listaDeErros = new List<RespostaErroDto>();

        if (string.IsNullOrWhiteSpace(entrada.Nome) || entrada.Nome.Length < 3)
        {
            var erro = new RespostaErroDto
            {
                Codigo = ErrosContants.COD_NOME_INVALIDO,
                Mensagem = ErrosContants.MSG_NOME_INVALIDO,
            };
            listaDeErros.Add(erro);
        }

        if (!MailAddress.TryCreate(entrada.Email, out var endereco) || !endereco.Host.Contains('.'))
        {
            var erro = new RespostaErroDto
            {
                Codigo = ErrosContants.COD_EMAIL_INVALIDO,
                Mensagem = ErrosContants.MSG_EMAIL_INVALIDO
            };
            listaDeErros.Add(erro);
        }

        if (listaDeErros.Count > 0)
        {
            var erroReponse = new SaidaErroDto
            {
                DadosResposta = listaDeErros,
                TempoDaResposta = "14s",
            };

            return erroReponse;
        }

        return true;
    }
}
