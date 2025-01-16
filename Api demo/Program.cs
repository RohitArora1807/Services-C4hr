using Api_demo.Models;
using Api_demo.Services;
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
builder.Services.AddScoped<IHelloService, HelloService>(); // Register HelloService
builder.Services.AddScoped<ICategoryService, CategoryService>();// Category service
builder.Services.AddScoped<IMWDATAService, MWDATAService>();// MWDATA service
builder.Services.AddScoped<IRRDETService, RRDETService>(); //RRDETService
builder.Services.AddDbContext<Db69605C4hr2Context>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


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