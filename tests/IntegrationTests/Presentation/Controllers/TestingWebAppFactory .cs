using System.Data.Common;
using Data;
using Data.Repositories;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace IntegrationTests.API.Tests.Controllers;

public class TestingWebApplicationFactory<TProgram>
    : WebApplicationFactory<TProgram>
    where TProgram : class
{
    public TestingWebApplicationFactory()
    {
        this.UnitOfWorkMock = new Mock<IUnitOfWork>();
    }

    public Mock<IUnitOfWork> UnitOfWorkMock { get; }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        base.ConfigureWebHost(builder);

        builder.ConfigureTestServices(services => services.AddSingleton(this.UnitOfWorkMock.Object));
    }
}
