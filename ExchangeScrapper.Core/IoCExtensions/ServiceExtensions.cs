namespace ExchangeScrapper.Core.IocExtensions;

public static class ServiceExtensions
{
    private const string JobsSection = "Jobs";
    private const string UseCasesSection = "UseCases";

    public static IServiceCollection AddCoreServices(this IServiceCollection services, IConfiguration configuration) 
        => services
            .AddJobs()
            .AddJobsConfiguration(configuration)
            .AddUseCases()
            .AddUseCasesConfiguration(configuration);

    private static IServiceCollection AddJobs(this IServiceCollection services)
    {
        services.AddSingleton<IPeriodicTimerJob, TradesSummaryJob>();
        return services;
    }

    private static IServiceCollection AddJobsConfiguration(this IServiceCollection services, IConfiguration configuration)
        => services.AddSingleton(configuration.GetSection(JobsSection).Get<JobsConfiguration>());

    private static IServiceCollection AddUseCases(this IServiceCollection services)
    {
        services.AddScoped<ICalculateTradesSummaryUseCase, TradesSummaryUseCase>();
        return services;
    }

    private static IServiceCollection AddUseCasesConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        var useCasesConfig = configuration.GetSection(UseCasesSection).Get<UseCasesConfiguration>();
        services.AddSingleton(useCasesConfig.TradesSummary);
        return services;
    }
}