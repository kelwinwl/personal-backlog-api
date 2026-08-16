using Microsoft.EntityFrameworkCore;
using PersonalBacklog.Api.Data;
using PersonalBacklog.Api.Services;
using PersonalBacklog.Api.Services.Interfaces;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IAnimeService, AnimeService>();

builder.Services.AddDbContext<BacklogDbContext>(options => 
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddControllers();

builder.Services.AddHttpClient<IExternalAnimeProvider, JikanApiService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();


app.MapGet("/", () => "Welcome to the Personal Backlog API!");

app.MapControllers();
app.Run();

