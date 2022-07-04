using System;
using System.Reflection;
using Domain;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using thegame.Models;
using thegame.Services;

var builder = WebApplication.CreateBuilder();

builder.Services.AddMvc();
builder.Services.AddAutoMapper(cfg =>
{
    cfg.CreateMap<Cell, CellDto>();
    cfg.CreateMap<Game, GameDto>();
}, Array.Empty<Assembly>());
builder.Services.AddSingleton<IGamesRepository, GamesRepository>();

var app = builder.Build();

app.UseDeveloperExceptionPage();
app.UseStaticFiles();
app.UseRouting();
app.UseEndpoints(endpoints => endpoints.MapControllers());
app.Use((context, next) =>
{
    context.Request.Path = "/index.html";
    return next();
});
app.UseStaticFiles();


app.Run();