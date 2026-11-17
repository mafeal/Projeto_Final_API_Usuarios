namespace Projeto_Final_API_Usuarios.Dtos;

public class SaidaUsuarioDto : SaidaPadraoDto
{
    public IEnumerable<RespostaUsuarioDto>? DadosResposta { get; set; }
}
