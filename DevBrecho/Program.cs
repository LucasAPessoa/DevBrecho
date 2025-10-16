using DevBrecho.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// ----- INÍCIO DO CÓDIGO DE DIAGNÓSTICO -----

// 1. Pega a string de conexão EXATAMENTE como a aplicação a lê.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// 2. Escreve a string de conexão diretamente no console/log de forma bem visível.
Console.WriteLine("----------------------------------------------------------");
Console.WriteLine($"--- MINHA CONNECTION STRING É: '{connectionString}' ---");
Console.WriteLine("----------------------------------------------------------");

// 3. Força a aplicação a quebrar com uma mensagem clara se a string for nula ou vazia.
if (string.IsNullOrEmpty(connectionString))
{
    throw new InvalidOperationException("A string de conexão 'DefaultConnection' NÃO FOI ENCONTRADA ou está VAZIA nas configurações. Verifique o nome da variável de ambiente (ConnectionStrings__DefaultConnection).");
}

// ----- FIM DO CÓDIGO DE DIAGNÓSTICO -----
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString));

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
