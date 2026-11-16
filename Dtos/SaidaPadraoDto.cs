namespace Projeto_Final_API_Usuarios.Dtos;

public class SaidaPadraoDto
{
    public DateTime TimestampResposta { get; set; } = DateTime.UtcNow;
    public string  TempoDaResposta { get; set; } = "";
}
