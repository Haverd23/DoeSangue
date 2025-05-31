using DOS.Doacao.API.Extensions;
using DOS.Doacao.Data;
using Microsoft.EntityFrameworkCore;
using Polly.Retry;
using Polly;



var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development";

if (environment == "Development")
{
    DotNetEnv.Env.Load();
}

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddEnvironmentVariables();

var secretKey = builder.Configuration["AppSettings:SecretKey"];
var connectionString = builder.Configuration["DEFAULT_CONNECTION"];

builder.Services.AddDependencyInjection(builder.Configuration);
builder.Services.AddJwtAuthentication(builder.Configuration);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<DoacaoContext>();

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