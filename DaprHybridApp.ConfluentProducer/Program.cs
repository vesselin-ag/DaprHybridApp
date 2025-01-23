using Confluent.Kafka;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();
builder.AddKafkaProducer<string, string>("messaging");

var app = builder.Build();

app.MapPost("/produce", async (string payload, IProducer<string, string> producer) =>
{
    await producer.ProduceAsync("messaging", new Message<string, string>()
    {
        Key = Guid.NewGuid().ToString(),
        Value = payload
    });

    return Results.Ok();
});

await app.RunAsync();

internal record MyEvent(string Value);