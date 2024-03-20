using Autofac.Extensions.DependencyInjection;
using DataInCloud.Dal;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Hosting;

namespace DataInCloud.IntegrationTests;

public class BaseTest : IDisposable
{
    protected IHost Host;
    protected AppDbContext AppDbContext;

    private IHostBuilder _server;

    public void Dispose()
    {
        StopServer();
    }

    public virtual HttpClient GetClient()
    {
        Host = _server.Start();
        AppDbContext = Host.Services.GetService(typeof(AppDbContext)) as AppDbContext;
        return Host.GetTestClient();
    }

    protected BaseTest InitTestServer()
    {
        _server = new HostBuilder()
            .UseServiceProviderFactory(new AutofacServiceProviderFactory())
            .UseEnvironment("Development")
            .ConfigureWebHost(webHost =>
            {
                webHost.UseTestServer();
                webHost.UseStartup<TestStartup>();
            });
        return this;
    }

    private void StopServer()
    {
        Host?.StopAsync().GetAwaiter().GetResult();
    }
}