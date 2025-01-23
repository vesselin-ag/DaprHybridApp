using System.Text.Json;
using Dapr;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();
builder.Services.AddDaprClient();

var app = builder.Build();

app.MapPost("/subscriptions/myEvent", (MyEvent myEvent) =>
{
    Console.WriteLine(JsonSerializer.Serialize(myEvent));
})
.WithTopic(new TopicOptions { PubsubName = "pubsub", Name = "messaging" });


await app.RunAsync();

internal record MyEvent(string Value);
