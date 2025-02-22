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



builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin() // Allow all origins
              .AllowAnyHeader() // Allow any headers
              .AllowAnyMethod(); // Allow any methods (GET, POST, etc.)

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
