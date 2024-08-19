using Context;
using Interfaces;
using Microsoft.EntityFrameworkCore;
using Model.Entities;
using Repository;
using Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddControllers(options =>
{
    options.ReturnHttpNotAcceptable = true; //Accetto solo il formato json
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add DBContext
var _connectionString = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
builder.Services.AddDbContext<LibraryContext>(options =>
    options.UseSqlServer(
        _connectionString.GetConnectionString("DefaultConnection"),
        b => b.MigrationsAssembly("WebApi")//Specifico l'assembly per le migrazioni
    )
);

builder.Services.AddTransient<IExtendedRepository<Book>, ExtendedRepository<Book>>();
builder.Services.AddScoped<IBookService, BookService>();

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
