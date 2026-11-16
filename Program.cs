var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUi(op => op.SwaggerEndpoint("/openapi/v1.json", "API - Usuários"));
}

app.UseHttpsRedirection();



app.Run();

