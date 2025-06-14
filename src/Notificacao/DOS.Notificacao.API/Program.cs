using DOS.Notificacao.Application.EventsHandlers.Usuario;
using DOS.Notificacao.Application.Kafka;
using DOS.Notificacao.Data;
using DOS.Notificacao.Domain;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
var emailSection = configuration.GetSection("Email");
var smtpHost = emailSection.GetValue<string>("smtpHost");
var smtpPort = emailSection.GetValue<int>("smtpPort");
var smtpUser = emailSection.GetValue<string>("smtpUser");
var smtpPass = emailSection.GetValue<string>("smtpPass");
var from = emailSection.GetValue<string>("from");

// Injeção do EmailService
builder.Services.AddSingleton<IEmailService>(provider =>
    new EmailService(smtpHost, smtpPort, smtpUser, smtpPass, smtpUseSsl: true));

// Add services to the container.
builder.Services.AddSingleton<KafkaEventRegistry>();
builder.Services.AddScoped<UsuarioCriadoEventHandler>(); 

builder.Services.AddHostedService<KafkaConsumerService>(provider =>
{
    var registry = provider.GetRequiredService<KafkaEventRegistry>();
    var bootstrapServers = "localhost:9092";
    var groupId = "notificacao-consumer-group";
    return new KafkaConsumerService(provider, registry, bootstrapServers, groupId);
});
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
