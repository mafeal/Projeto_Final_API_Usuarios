using Projeto_Final_API_Usuarios.Endpoints;
using Projeto_Final_API_Usuarios.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI(op => op.SwaggerEndpoint("/openapi/v1.json", "API - Usuários"));
}

app.UseHttpsRedirection();

UsuarioRepository.CarregarDadosNoDicionario();

app.MapUsuarios();

app.Run();

