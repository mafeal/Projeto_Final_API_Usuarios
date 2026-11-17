namespace Projeto_Final_API_Usuarios.Models;

public class Usuario
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Nome { get; set; } = "";
    public string Email { get; set; } = "";
    public string SenhaHash { get; set; } = "";
    public bool Ativo { get; set; } = true;
    public string CriadoEm { get; set; } = DateTime.UtcNow.ToString("dd-MM-yyyy HH:mm:ss");
    public string? AtualizadoEm { get; set; }
    public string Cargo { get; set; } = "";
}