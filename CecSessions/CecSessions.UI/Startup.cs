using CecSessions.Core.Entities;
using CecSessions.UI.Schedule;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PecMembers.UI.Areas.Identity;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CecSessions.UI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            ServicesInitializer.ConfigureDbContext(Configuration, services);

            //add to injections

            ServicesInitializer.ConfigureServices(services);

            ////part of Session
            //services.AddHttpContextAccessor();
            //services.AddSession(s => s.IdleTimeout = TimeSpan.FromMinutes(30));
            //services.AddDistributedMemoryCache();
            //services.AddSession();


            //part of Identity
            services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
              .AddRoles<IdentityRole>()
               .AddEntityFrameworkStores<CecSessionsContext>();

            services.AddScoped<AuthenticationStateProvider, RevalidatingIdentityAuthenticationStateProvider<ApplicationUser>>();








            // Add the required Quartz.NET services
            services.AddQuartz(q =>
            {
                // Use a Scoped container to create jobs. I'll touch on this later
                q.UseMicrosoftDependencyInjectionScopedJobFactory();

                // Create a "key" for the job
                var testExecuteJobKey = new JobKey("TestExecuteJob");

                // Register the job with the DI container
                q.AddJob<TestExecuteJob>(opts => opts.WithIdentity(testExecuteJobKey));

                // Create a trigger for the job
                q.AddTrigger(opts => opts
                    .ForJob(testExecuteJobKey) // link to the ParliamentResultJob
                    .WithIdentity("TestExecuteJob-trigger") // give the trigger a unique name
                    .WithCronSchedule("0/10 * * * * ?") // run every 30 seconds
                    );
            });
            // Add the Quartz.NET hosted service

            services.AddQuartzHostedService(
                q => q.WaitForJobsToComplete = true);

            // other config
            //services.AddSingleton<IJobFactory, JobFactory>();
            //services.AddSingleton<ISchedulerFactory, StdSchedulerFactory>();
            //services.AddSingleton<TestExecuteJob>();
            //services.AddSingleton(new JobMetadata(Guid.NewGuid(), typeof(TestExecuteJob), "Notification Job", "0/30 * * * * ?"));
            //services.AddHostedService<HostedService>();








            services.AddRazorPages();
         

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
        }
    }
}
