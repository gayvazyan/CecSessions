using System.IO;

namespace CecSessions.UI.Common
{
    public static class CreateFile
    {
        public static void Logs(string message, string fileName)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "MyLogs");

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            path = Path.Combine(path, fileName);

            using FileStream fileStream = new FileStream(path, FileMode.Create);
            using TextWriter textWriter = new StreamWriter(fileStream);
            textWriter.WriteLine(message);
        }
    }
}
