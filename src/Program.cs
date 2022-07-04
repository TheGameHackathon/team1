using System;
using System.Reflection;
using Domain;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using thegame.Models;
using thegame.Services;

var builder = WebApplication.CreateBuilder();

builder.Services.AddMvc();
builder.Services.AddControllersWithViews()
    .AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
        options.SerializerSettings.DefaultValueHandling = DefaultValueHandling.Populate;
    });

builder.Services.AddAutoMapper(cfg =>
{
    cfg.CreateMap<Vector, VectorDto>();
    cfg.CreateMap<Cell, CellDto>()
        .ForMember(dest => dest.Content, 
            opt => opt.MapFrom(
            src => src.Value == 0 ? "" : src.Value.ToString()))
        .ForMember(dest => dest.Type, opt => opt.MapFrom(src => "color" + src.Value));
    cfg.CreateMap<Game, GameDto>();
}, Array.Empty<Assembly>());
builder.Services.AddSingleton<IGamesRepository, GamesRepository>();
builder.Services.AddScoped<IBot, LeftUpBot>();

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