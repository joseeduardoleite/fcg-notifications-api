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

        cfg.UseMessageRetry(r =>
        {
            r.Exponential(
                retryLimit: 5,
                minInterval: TimeSpan.FromSeconds(1),
                maxInterval: TimeSpan.FromSeconds(5),
                intervalDelta: TimeSpan.FromSeconds(1)
            );
        });

        cfg.ReceiveEndpoint("notifications-usercreated-queue", e =>
        {
            e.ConfigureConsumer<UserCreatedEventConsumer>(context);
        });

        cfg.ReceiveEndpoint("notifications-payment-approved-queue", e =>
        {
            e.ConfigureConsumer<PaymentApprovedEventConsumer>(context);
        });

        cfg.ReceiveEndpoint("notifications-payment-rejected-queue", e =>
        {
            e.ConfigureConsumer<PaymentRejectedEventConsumer>(context);
        });
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
