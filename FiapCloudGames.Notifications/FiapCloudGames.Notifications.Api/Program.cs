using FiapCloudGames.Notifications.Application;
using FiapCloudGames.Notifications.Infrastructure;
using FiapCloudGames.Notifications.Infrastructure.Messaging;
using MassTransit;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplicationModule();
builder.Services.AddInfraModule(builder.Configuration);

builder.Services.AddAuthorization();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<PaymentApprovedEventConsumer>();
    x.AddConsumer<PaymentRejectedEventConsumer>();
    x.AddConsumer<UserCreatedEventConsumer>();

    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host(builder.Configuration["RabbitMQ:Host"], "/", h =>
        {
            h.Username(builder.Configuration["RabbitMQ:Username"]!);
            h.Password(builder.Configuration["RabbitMQ:Password"]!);
        });

        cfg.ConfigureEndpoints(context);
    });
});

WebApplication app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

await app.RunAsync();
