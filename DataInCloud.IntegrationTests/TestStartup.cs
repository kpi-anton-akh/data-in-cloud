﻿using DataInCloud.Dal;
using DataOnCloud.Api;
using EntityFrameworkCore.Testing.Common.Helpers;
using EntityFrameworkCore.Testing.Moq.Helpers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DataInCloud.IntegrationTests;

public class TestStartup : Startup
{
    public TestStartup(IConfiguration configuration, IWebHostEnvironment env) 
        : base(configuration, env)
    {
    }

    protected override void ConfigureDb(IServiceCollection services)
    {
        var context = ConfigureDb<AppDbContext>().MockedDbContext;
        services.AddSingleton<AppDbContext>(c => context);
    }

    private IMockedDbContextBuilder<T> ConfigureDb<T>()
        where T : DbContext
    {
        var options = new DbContextOptionsBuilder<T>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
        var dbContextToMock = (T)Activator.CreateInstance(typeof(T), options);
        return new MockedDbContextBuilder<T>()
            .UseDbContext(dbContextToMock)
            .UseConstructorWithParameters(options);
    }
}