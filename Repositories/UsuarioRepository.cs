using System.Collections.Concurrent;
using Projeto_Final_API_Usuarios.Models;

namespace Projeto_Final_API_Usuarios.Repositories;

public static class UsuarioRepository
{
    public static ConcurrentDictionary<Guid, Usuario> UsuariosData = new();

    public static void CarregarDadosNoDicionario()
    {
        var usuario1 = new Usuario
        {
            Id = Guid.NewGuid(),
            Nome = "Carlos Drumont",
            Email = "carlos.drumont@andrade.com",
            SenhaHash = "#Senha_12345678",
            CriadoEm = new DateTime(2025, 10, 16, 15, 30, 0),
            Cargo = "Escritor"
        };

        var usuario2 = new Usuario
        {
            Id = Guid.NewGuid(),
            Nome = "Elvis Presley",
            Email = "elvis.presley@naomorreu.com",
            SenhaHash = "#Senha_87654321",
            CriadoEm = new DateTime(2026, 02, 27, 11, 14, 0),
            Cargo = "Cantor"
        };

        var usuario3 = new Usuario
        {
            Id = Guid.NewGuid(),
            Nome = "Leonardo Da Vinci",
            Email = "leonardo.davinci@monalisa.com",
            SenhaHash = "#Senha_12457845",
            CriadoEm = new DateTime(2026, 07, 09, 16, 45, 0),
            Cargo = "Pintor"
        };

        UsuariosData.TryAdd(usuario1.Id, usuario1);
        UsuariosData.TryAdd(usuario2.Id, usuario2);
        UsuariosData.TryAdd(usuario3.Id, usuario3);

    }
}
