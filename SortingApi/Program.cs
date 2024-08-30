using Microsoft.EntityFrameworkCore;
using Application.Services;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Infrastructure.Repositories;
using SortingApi.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IArrayRepository, ArrayTextFileRepository>();
builder.Services.AddScoped<IDataProcessingService, DataProcessingService>();
builder.Services.AddScoped<SortingService>();
builder.Services.AddScoped<ISortingService>(x =>
    new TimedSortingDecorator(x.GetRequiredService<SortingService>()));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
