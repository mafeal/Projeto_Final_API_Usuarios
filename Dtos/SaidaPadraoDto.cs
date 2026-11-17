namespace Projeto_Final_API_Usuarios.Dtos;

public class SaidaPadraoDto
{
    public string TimestampResposta { get; set; } = DateTime.UtcNow.ToString("dd-MM-yyyy HH:mm:ss");
    public string TempoDaResposta { get; set; } = "";
}
