using JernaClassLib.IServices;
using JernaIntegrationTests.TestServices;
using JernaWebApp.Data;
using JernaWebApp.IWebServices;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Testcontainers.PostgreSql;

namespace JernaIntegrationTests;

public class JernaWebAppFactory : WebApplicationFactory<JernaWebApp.Program>, IAsyncLifetime
{
    private readonly PostgreSqlContainer _dbContainer;
    public JernaWebAppFactory()
    {
        var backupFile = Directory.GetFiles("./../../..", "*.sql", SearchOption.AllDirectories)
            .Select(f => new FileInfo(f))
            .OrderByDescending(f => f.LastWriteTime)
            .First();

        _dbContainer = new PostgreSqlBuilder()
            .WithImage("postgres")
            .WithPassword("P@ssword1")
            .WithBindMount(backupFile.FullName, "/docker-entrypoint-initdb.d/init.sql")
            .Build();
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureTestServices(services =>
        {
            services.RemoveAll(typeof(IDbContextFactory<JernaContext>));
            services.RemoveAll(typeof(ISecureStorageService));
            services.RemoveAll(typeof(IWebAuthUtilityService));
            services.RemoveAll(typeof(IEmailService));

            services.AddScoped<ISecureStorageService, TestSecureService>();
            services.AddScoped<IWebAuthUtilityService, TestWebAuthUtilService>();
            services.AddScoped<IEmailService, TestEmailService>();
            services.AddDbContextFactory<JernaContext>(o =>
            {
                o.UseNpgsql(_dbContainer.GetConnectionString());
            });
        });
    }

    public async Task InitializeAsync()
    {
        await _dbContainer.StartAsync();
    }

    async Task IAsyncLifetime.DisposeAsync() => await _dbContainer.StopAsync();
}
