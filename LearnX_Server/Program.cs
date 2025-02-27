using System.Text.Json.Serialization;
using LearnX_Server;
using LearnX_Server.Data;
using LearnX_Server.Interfaces;
using LearnX_Server.Services;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();


builder.Services.AddDbContext<DbConnection>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("Sql")));
builder.Services.AddSingleton<ITokenService, TokenService>();
builder.Services.AddSingleton<IMailService, MailService>();
builder.Services.AddScoped<IMessageHandler, MessageHandler>();
builder.Services.AddSingleton<ICloudnaryServices, CloudnaryServices>();



builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.WithOrigins("http://localhost:5173") // React frontend URL
             .AllowCredentials() // Allow cookies
             .AllowAnyHeader() // Allow headers
             .AllowAnyMethod(); // Allow all methods

    });
});


builder.Services.AddSwaggerGen();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

app.Run();
