namespace Projeto_Final_API_Usuarios.Dtos;

public class RespostaUsuarioDto
{
    public Guid Id { get; set; }
    public string Nome { get; set; } = "";
    public string Email { get; set; } = "";
    public bool Ativo { get; set; }
    public string CriadoEm { get; set; }
    public string? AtualizadoEm { get; set; }
    public string? Cargo { get; set; }
}
