using System.Text.Json;
using System.Text.Json.Serialization;
using Projeto_Final_API_Usuarios.Endpoints;
using Projeto_Final_API_Usuarios.Repositories;
using Projeto_Final_API_Usuarios.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddScoped<UsuarioService>();
builder.Services.AddSingleton<UsuarioRepository>();

builder.Services.ConfigureHttpJsonOptions(options =>
{
    // camelCase nas propriedades (já é o padrão no ASP.NET Core; explícito aqui para a aula)
    options.SerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower;

    // omitir propriedades nulas na resposta
    options.SerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;

    // serializar e aceitar enums como string
    options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());

    // aceitar números escritos como string na leitura ("123" -> 123)
    options.SerializerOptions.NumberHandling = JsonNumberHandling.AllowReadingFromString;
});

var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI(op => op.SwaggerEndpoint("/openapi/v1.json", "API - Usuários"));
}

app.UseHttpsRedirection();
app.MapUsuarios();
app.Run();

