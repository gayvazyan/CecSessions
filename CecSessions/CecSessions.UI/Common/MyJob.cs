using System;

namespace CecSessions.UI.Common
{
    public class MyJob
    {
        public Type Type  { get;  }
        public string Expression { get;  }
        public MyJob(Type type, string expression)
        {

            CreateFile.Logs($"My Job at" + DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss"), "My_Job_" + DateTime.Now.ToString("hh_mm_ss"));

            Type = type;
            Expression = expression;
        }
    }
}
