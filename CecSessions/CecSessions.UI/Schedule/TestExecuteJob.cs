using CecSessions.UI.Common;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using Quartz;
using System;
using System.Threading.Tasks;

namespace CecSessions.UI.Schedule
{
    [DisallowConcurrentExecution]
    public class TestExecuteJob : IJob
    {
        private readonly ILogger<TestExecuteJob> _logger;
        private readonly IWebHostEnvironment _env;
        public TestExecuteJob(ILogger<TestExecuteJob> logger,
           IWebHostEnvironment env
           )
        {
            _env = env;
            _logger = logger;
          
        }

        public Task Execute(IJobExecutionContext context)
        {
           // CreateFile.Logs($"Job Execute Time at" + DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss"), "Job_Execute_" + DateTime.Now.ToString("hh_mm_ss"));
            var result = $"Job_ExecuteTime_" + DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss");
            _logger.LogInformation(result);
            return Task.CompletedTask;
        }
    }
}
