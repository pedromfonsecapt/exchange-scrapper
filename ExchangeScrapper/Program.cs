using ExchangeScrapper.Common.IoCExtensions;

var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", false)
    .AddEnvironmentVariables()
    .Build();

var services = new ServiceCollection()
    .AddLogging(loggingBuilder =>
    {
        loggingBuilder.ClearProviders();
        loggingBuilder.AddSerilog(dispose: true);
    })
    .AddSingleton<IConfiguration>(configuration)
    .AddCommonServices()
    .AddBitfinexConnector(configuration)
    .AddKrakenConnector(configuration)
    .AddCoreServices(configuration)
    .AddDomainServices()
    .AddFeatureManagement()
    .Services
    .AddHealthChecks(configuration)
    .BuildServiceProvider();

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Verbose()
    .ReadFrom.Configuration(configuration)
    .ReadFrom.Services(services)
    .Enrich.FromLogContext()
    .Enrich.WithExceptionDetails()
    .Enrich.WithSpan()
    .Enrich.WithThreadId()
    .WriteTo.Console(new JsonFormatter())
    .CreateLogger();


var logger = services.GetRequiredService<ILogger<Program>>();
var cts = new CancellationTokenSource();
Console.CancelKeyPress += (s, e) =>
{
    logger.LogInformation("Canceling...");
    cts.Cancel();
    e.Cancel = true;
};
logger.LogInformation("Starting Exchange Scrapper");
var healthCheckService = services.GetRequiredService<HealthCheckService>();
var healthCheckResult = await healthCheckService.CheckHealthAsync(cts.Token);
if (healthCheckResult.Status == HealthStatus.Unhealthy)
{
    logger.LogCritical("Health Check Failed: {@healthCheck}", healthCheckResult);
    Environment.Exit(1);
}
var jobs = services.GetServices<IPeriodicTimerJob>();
foreach (var job in jobs)
{
#pragma warning disable CS4014
    job.Start(cts.Token);
#pragma warning restore CS4014
}

while (!cts.Token.IsCancellationRequested) { }

Environment.Exit(0);
