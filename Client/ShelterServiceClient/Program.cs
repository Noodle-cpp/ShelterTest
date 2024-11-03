using Microsoft.Extensions.Logging.Configuration;
using Microsoft.Extensions.Logging.EventLog;
using ShelterServiceClient;
using ShelterServiceClient.Utilities;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddWindowsService(options =>
{
    options.ServiceName = "SHELTER-Test Service";
});

LoggerProviderOptions.RegisterProviderOptions<EventLogSettings, EventLogLoggerProvider>(builder.Services);

builder.Services.AddHostedService<Worker>();

builder.Logging.AddConfiguration(
    builder.Configuration.GetSection("Logging"));

builder.Services.Configure<ShelterServiceConfigurationOptions>(builder.Configuration);

builder.Services.AddSingleton<IHttpRequests, HttpRequests>();
builder.Services.AddHttpClient();

var host = builder.Build();
host.Run();
