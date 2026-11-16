namespace Projeto_Final_API_Usuarios.Models;

public class Usuario
{
    public Guid Id { get; set; } = new Guid();
    public string Nome { get; set; } = "";
    public string Email { get; set; } = "";
    public string SenhaHash { get; set; } = "";
    public bool Ativo { get; set; } = true;
    public DateTime CriadoEm { get; set; } = DateTime.UtcNow;
    public DateTime? AtualizadoEm { get; set; }
    public string Cargo { get; set; } = "";
}