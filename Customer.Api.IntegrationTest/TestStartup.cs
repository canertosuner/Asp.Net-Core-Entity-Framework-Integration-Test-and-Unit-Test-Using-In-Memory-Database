using Customer.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Customer.Api.IntegrationTest
{
    public class TestStartup : Startup
    {
        public TestStartup(IConfiguration configuration) : base(configuration)
        {
        }

        public override void ConfigureDatabase(IServiceCollection services)
        {
            services.AddDbContext<CustomerDbContext>(options =>
                options.UseInMemoryDatabase("customerDb_test"));
        }
    }
}
