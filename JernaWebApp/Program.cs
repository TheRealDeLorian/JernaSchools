using JernaWebApp.Components;
using JernaClassLib.IServices;
using JernaWebApp.Services;
using Microsoft.EntityFrameworkCore;
using JernaWebApp.Data;
using JernaWebApp.IWebServices;
using JernaClassLib.Components;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Text.Json.Serialization;
using OpenTelemetry.Logs;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using OpenTelemetry.Metrics;
using JernaClassLib;
using System.Text.Json;
using JernaClassLib.MetricsNLogs;
namespace JernaWebApp;

public class Program
{
    public static void Main(string[] args)
    {

        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddScoped<IEmailService, EmailService>();
        builder.Services.AddScoped<IAuthService, WebAuthService>();
        builder.Services.AddScoped<IItemService, WebItemService>();
        builder.Services.AddScoped<WebItemService>();
        builder.Services.AddScoped<UserService>();
        builder.Services.AddScoped<IWebAuthUtilityService, WebAuthUtilityService>();
        builder.Services.AddScoped<ICartService, WebCartService>();
        builder.Services.AddScoped<IPurchaseService, WebPurchaseService>();
        builder.Services.AddScoped<ITagService, WebTagService>();
        builder.Services.AddScoped<EventService>();
        builder.Services.AddHttpClient();
        builder.Services.AddDbContextFactory<JernaContext>(o =>
        {
            o.UseNpgsql(builder.Configuration["db"]);
        });
        builder.Services.AddScoped<ISecureStorageService, WebSecureStorageService>();
        builder.Services.AddControllers().AddJsonOptions(x =>
           x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

        builder.Services.AddRazorComponents()
        .AddInteractiveServerComponents();
        builder.Services.AddScoped<JernaAuthState>();
        builder.Configuration[Constants.ApplicationType] = Constants.WebApplication;

        builder.Services.AddCascadingValue("JernaAuthState", sp =>
        {
            var dalek = sp.GetRequiredService<JernaAuthState>();
            return dalek;
        });

        // add singletons for each Metrics class
        builder.Services.AddMetrics();
        builder.Services.AddLogging();
        builder.Services.AddHealthChecks();
        builder.Services.AddSingleton<JernaMetrics>();

        const string telemetryServiceName = "JERNATelemetryServiceForBlazorApp";

        builder.Logging.AddOpenTelemetry(options =>
            {
                options
                    .SetResourceBuilder(
                        ResourceBuilder.CreateDefault().AddService(telemetryServiceName))
                    .AddOtlpExporter(opt =>
                    {
                        opt.Endpoint = new Uri("http://otel-collector:4317/");
                    })
                    .AddConsoleExporter();
            });

        builder.Services.AddOpenTelemetry()
            .ConfigureResource(resource =>
            resource.AddService(telemetryServiceName))
            .WithTracing(b => b
                .AddSource(TracingService.ActivitySourceName)
                .AddAspNetCoreInstrumentation()
                .AddOtlpExporter(opt =>
                {
                    opt.Endpoint = new Uri("http://otel-collector:4137/");
                })
            )
            .WithMetrics(metrics => metrics
                .AddAspNetCoreInstrumentation()
                .AddMeter(JernaMetrics.JernaMeterName) //add new meters here
                .AddPrometheusExporter()
                .AddOtlpExporter(opt =>
                {
                    opt.Endpoint = new Uri("http://otel-collector:4317/");
                }));

        var app = builder.Build();

        app.UseOpenTelemetryPrometheusScrapingEndpoint();
        app.MapPrometheusScrapingEndpoint();

        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Error", createScopeForErrors: true);
            app.UseHsts();
        }

        var logger = app.Services.GetRequiredService<ILogger<Program>>();

        app.MapHealthChecks("/health", new HealthCheckOptions
        {
            AllowCachingResponses = false,
            ResultStatusCodes =
                {
                    [HealthStatus.Healthy] = StatusCodes.Status200OK,
                    [HealthStatus.Degraded] = StatusCodes.Status200OK,
                    [HealthStatus.Unhealthy] = StatusCodes.Status503ServiceUnavailable
                },
            ResponseWriter = async (context, report) =>
                {
                    context.Response.ContentType = "application/json";

                    // Log the health check result
                    if (report.Status == HealthStatus.Healthy)
                    {
                        logger.LogInformation("Health check is healthy");
                    }
                    else if (report.Status == HealthStatus.Degraded)
                    {
                        logger.LogWarning("Health check is degraded");
                    }
                    else
                    {
                        logger.LogError("Health check is unhealthy");
                    }

                    // Write the health check report to the response
                    var result = JsonSerializer.Serialize(new
                    {
                        status = report.Status.ToString(),
                        checks = report.Entries.Select(e => new
                        {
                            name = e.Key,
                            status = e.Value.Status.ToString(),
                            description = e.Value.Description
                        }),
                        duration = report.TotalDuration
                    });

                    await context.Response.WriteAsync(result);
                }
        });


        JernaLogs.LogApplicationAccess(app.Logger, "JernaWebApp. It's working!");

        app.UseHttpsRedirection();

        app.UseStaticFiles();
        app.UseAntiforgery();

        app.MapControllers();

        app.MapRazorComponents<App>()
            .AddAdditionalAssemblies(typeof(About).Assembly)
            .AddInteractiveServerRenderMode();

        app.Run();
    }
}

