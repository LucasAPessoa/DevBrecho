using DevBrecho.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// ----- IN�CIO DO C�DIGO DE DIAGN�STICO -----

// 1. Pega a string de conex�o EXATAMENTE como a aplica��o a l�.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// 2. Escreve a string de conex�o diretamente no console/log de forma bem vis�vel.
Console.WriteLine("----------------------------------------------------------");
Console.WriteLine($"--- MINHA CONNECTION STRING �: '{connectionString}' ---");
Console.WriteLine("----------------------------------------------------------");

// 3. For�a a aplica��o a quebrar com uma mensagem clara se a string for nula ou vazia.
if (string.IsNullOrEmpty(connectionString))
{
    throw new InvalidOperationException("A string de conex�o 'DefaultConnection' N�O FOI ENCONTRADA ou est� VAZIA nas configura��es. Verifique o nome da vari�vel de ambiente (ConnectionStrings__DefaultConnection).");
}

// ----- FIM DO C�DIGO DE DIAGN�STICO -----
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
