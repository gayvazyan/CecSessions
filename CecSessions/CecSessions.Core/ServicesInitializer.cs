using CecSessions.Core;
using CecSessions.Core.Entities;
using CecSessions.Core.Services.ProcedureTest;
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


            //services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
            //  .AddRoles<IdentityRole>()
            //   .AddEntityFrameworkStores<CecSessionsContext>();

           
        }

        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient(typeof(IRepositories<>), typeof(Repositories<>));
            
            services.AddTransient<ISessionRepasitory, SessionRepasitory>();
            services.AddTransient<IQuestionRepasitory, QuestionRepasitory>();
            services.AddTransient<IResultRepasitory, ResultRepasitory>();

            services.AddTransient<IProcedureTest, ProcedureTest>();

        }
    }
}