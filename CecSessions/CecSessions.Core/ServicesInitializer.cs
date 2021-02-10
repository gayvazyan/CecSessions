using CecSessions.Core;
using CecSessions.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CecSessions
{
    public static partial class ServicesInitializer
    {
        public static void ConfigureDbContext(IConfiguration configuration, IServiceCollection services)
        {

            services.AddDbContextPool<CecSessionsContext>(
              options => options.UseSqlServer(configuration.GetConnectionString("CecSessionsDbConnection")));

        }

        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient(typeof(IRepositories<>), typeof(Repositories<>));
            
            services.AddTransient<IUserRepasitory, UserRepasitory>();

        }
    }
}