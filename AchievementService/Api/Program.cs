using System.Reflection;
using Api.Extensions;
using Api.Models.Request;
using Dal;
using Dal.Entities;
using Logic.Entities;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(options =>
{
    options.OutputFormatters.Add(new XmlDataContractSerializerOutputFormatter());
    options.ReturnHttpNotAcceptable = true;
    options.RespectBrowserAcceptHeader = true;
});

builder.Services.AddAutoMapper(config =>
{
    config.CreateMap<AchievementDal, AchievementDto>().ReverseMap();
    config.CreateMap<CreateUpdateAchievementRequest, AchievementDto>();
}, Array.Empty<Assembly>());

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AchievementContext>(options =>
{
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddUserAndAchievementLogicServices();
builder.Services.AddUserAndAchievementRepositoriesWithManager();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

app.Run();