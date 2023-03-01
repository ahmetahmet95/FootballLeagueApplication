using DataAccess.Interface;
using DataAccess.Service;
using DataAccessLibrary;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ServiceLibrary.Interfaces;
using ServiceLibrary.Services;
using System.Net;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DbConnection");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
builder.Services.AddScoped(typeof(ITeamService), typeof(TeamService));

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();


var origins = builder.Configuration.GetSection("Origins").GetChildren().Select(c => c.Value);

app.UseCors(builder => builder
                .AllowAnyHeader()
                .WithExposedHeaders("Content-Disposition", "X-Apollo-Editable", "X-Apollo-Version", "X-Filename")
                .AllowAnyMethod()
                .AllowCredentials()
                //.AllowAnyOrigin()
                .WithOrigins(origins.ToArray())
                );

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseExceptionHandler(builder =>
{
    builder.Run(
      async context =>
      {
          context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
          context.Response.ContentType = "text/html";

          var error = context.Features.Get<IExceptionHandlerFeature>();
          if (error != null)
          {
              await
                  context.Response.WriteAsync($"Error: {error.Error.Message}")
                      .ConfigureAwait(false);
          }
      });
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
