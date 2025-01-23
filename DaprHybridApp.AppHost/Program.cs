using Projects;

var builder = DistributedApplication.CreateBuilder(args);
var kafka = builder.AddKafka("kafka")
    .WithKafkaUI(ui => ui.WithHostPort(9100))
    .WithDataVolume(isReadOnly: false);

builder.AddProject<DaprHybridApp_ConfluentProducer>("producer")
    .WithReference(kafka);

builder.AddProject<DaprHybridApp_DaprSubscriber>("subscriber")
    .WithDaprSidecar()
    .WithReference(kafka);

await builder.Build().RunAsync();