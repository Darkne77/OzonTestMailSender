
using Newtonsoft.Json.Serialization;
using OzonTestMailSender.Core.BL;
using OzonTestMailSender.Core.Repositories;
using OzonTestMailSender.Core.Services;
using OzonTestMailSender.Infrastructure.Models;
using OzonTestMailSender.Infrastructure.Repositories;
using OzonTestMailSender.Infrastructure.Services;
using OzonTestMailSender.MappingProfiles;
using ExceptionHandlerMiddleware = OzonTestMailSender.Middlewares.ExceptionHandlerMiddleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddNewtonsoftJson(options=>
{
    options.SerializerSettings.ContractResolver = new DefaultContractResolver()
    {
        NamingStrategy = new SnakeCaseNamingStrategy()
    };
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen().AddSwaggerGenNewtonsoftSupport();

builder.Services.Configure<SenderCredentials>(builder.Configuration.GetSection(nameof(SenderCredentials)));
builder.Services.Configure<ConnectionStrings>(builder.Configuration.GetSection(nameof(ConnectionStrings)));

builder.Services.AddSingleton<IEmailSender, EmailSender>();
builder.Services.AddSingleton<IEmailService, EmailService>();
builder.Services.AddSingleton<IMessageHistoryRepository, MessageHistoryRepository>();

builder.Services.AddAutoMapper(typeof(SendEmailResultProfile));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionHandlerMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();