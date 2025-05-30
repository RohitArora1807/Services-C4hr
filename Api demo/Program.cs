using Api_demo.Logging;
using Api_demo.Models;
using Api_demo.Services;
using C4HR_KB_PROJECT_KPMG.ModelsKB;
using Microsoft.EntityFrameworkCore;
using System;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add services to the container.
builder.Services.AddControllers();
//builder.Services.AddScoped<IHelloService, HelloService>(); // Register HelloService
builder.Services.AddScoped<ICategoryService, CategoryService>();// Category service
builder.Services.AddScoped<IMWDATAService, MWDATAService>();// MWDATA service
builder.Services.AddScoped<IRRDETService, RRDETService>(); //RRDETService
builder.Services.AddScoped<IActService, ActService>(); //ActService
builder.Services.AddDbContext<DbC4HRContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DB_69605_c4hr2")));

builder.Services.AddDbContext<DbC4HRKBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("C4HRKPMGKB")));
var logDirectory = Path.Combine(Directory.GetCurrentDirectory(), "Logs");
builder.Services.AddSingleton(new LoggerService(logDirectory));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
if (app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();