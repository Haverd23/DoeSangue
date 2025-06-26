using DOS.Agenda.API.Extensions;
using DOS.Agenda.Data;
using DOS.Core.Exceptions;
using Microsoft.EntityFrameworkCore;
using Polly;
using Polly.Retry;


var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development";

if (environment == "Development")
{
    DotNetEnv.Env.Load();
}

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddEnvironmentVariables();



builder.Services.AddDependencyInjection(builder.Configuration);
builder.Services.AddJwtAuthentication(builder.Configuration);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.UseMiddleware<ExceptionMiddleware>();


using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<HorarioContext>();

    RetryPolicy retryPolicy = Policy
        .Handle<Exception>()
        .WaitAndRetry(5, retryAttempt => TimeSpan.FromSeconds(5),
            (exception, timeSpan, retryCount, contextLog) =>
            {
                Console.WriteLine($"Tentativa {retryCount} falhou. Aguardando {timeSpan.TotalSeconds}s...");
                Console.WriteLine($"Erro: {exception.Message}");
            });

    retryPolicy.Execute(() =>
    {
        context.Database.Migrate();
    });
}

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.Run();