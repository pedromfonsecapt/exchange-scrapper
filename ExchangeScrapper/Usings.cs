global using ExchangeScrapper.BitfinexConnector.IoCExtensions;
global using Microsoft.Extensions.Diagnostics.HealthChecks;
global using Serilog;
global using Serilog.Enrichers.Span;
global using Serilog.Exceptions;
global using Serilog.Formatting.Json;
global using ExchangeScrapper.BitfinexConnector.Configuration;
global using Microsoft.Extensions.Configuration;
global using Microsoft.Extensions.DependencyInjection;
global using Microsoft.Extensions.Logging;
global using ExchangeScrapper.Interfaces.Core;
global using ExchangeScrapper.KrakenConnector.IoCExtensions;
global using ExchangeScrapper;
global using ExchangeScrapper.Core.IocExtensions;
global using ExchangeScrapper.Domain.Services.IocExtensions;
global using Microsoft.FeatureManagement;