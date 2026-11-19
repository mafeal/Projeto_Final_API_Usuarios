namespace Projeto_Final_API_Usuarios.Dtos;

public class SaidaErroDto : SaidaPadraoDto
{
    public List<RespostaErroDto> DadosResposta { get; set; } = [];
}
