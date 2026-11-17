using System.Collections.Concurrent;
using Projeto_Final_API_Usuarios.Models;

namespace Projeto_Final_API_Usuarios.Repositories;

public class UsuarioRepository
{
    public Dictionary<Guid, Usuario> UsuariosData = new();
    public void CarregarDadosMockadosNoDicionario()
    {
        var usuario1 = new Usuario
        {
            Id = Guid.Parse("7c7590ae-6997-4e74-9d70-5f9a92271909"),
            Nome = "Carlos Drumont",
            Email = "carlos.drumont@andrade.com",
            SenhaHash = "#Senha_12345678",
            CriadoEm = "16-10-2025 15:30:00",
            Cargo = "Escritor"
        };

        var usuario2 = new Usuario
        {
            Id = Guid.Parse("57d11c73-d5a0-4b8d-a752-749cc82378cc"),
            Nome = "Elvis Presley",
            Email = "elvis.presley@naomorreu.com",
            SenhaHash = "#Senha_87654321",
            CriadoEm = "27-02-2026 11:14:00",
            Cargo = "Cantor"
        };

        var usuario3 = new Usuario
        {
            Id = Guid.Parse("075568d8-2c99-400e-93e0-8bb23b2c0925"),
            Nome = "Leonardo Da Vinci",
            Email = "leonardo.davinci@monalisa.com",
            SenhaHash = "#Senha_12457845",
            CriadoEm = "09-07-2026 16:45:00",
            Cargo = "Pintor"
        };

        UsuariosData.TryAdd(usuario1.Id, usuario1);
        UsuariosData.TryAdd(usuario2.Id, usuario2);
        UsuariosData.TryAdd(usuario3.Id, usuario3);

    }
}
