namespace Projeto_Final_API_Usuarios.Dtos;

public class EntradaUsuarioDto
{
    public required string Nome { get; set; }
    public required string Email { get; set; }
    public required string Senha { get; set; }
    public string? Cargo { get; set; }
}
