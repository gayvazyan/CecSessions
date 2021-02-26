using System.Collections.Generic;
using System.IO;
using CecSessions.Core.Models.Json;
using CecSessions.Core.Services.ProcedureTest;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CecSessions.UI.Pages.Client.Json
{
    public class IndexModel : PageModel
    {
       
        private readonly IWebHostEnvironment _env;
        public IndexModel(IWebHostEnvironment env)
        {
            _env = env;
            JsonList = new List<ResultDataJson>();
        }

       

        [BindProperty]
        public List<ResultDataJson> JsonList { get; set; }


        public void OnGet()
        {
            //-----------Start of Prepare Folder
            var uploadDir = Path.Combine(_env.WebRootPath, "json");
            var fileName = "result.json";
            string filePath = Path.Combine(uploadDir, fileName);
            if (System.IO.File.Exists(filePath))
            {
                var fileContent = System.IO.File.ReadAllText(filePath);
                JsonList = Newtonsoft.Json.JsonConvert.DeserializeObject<List<ResultDataJson>>(fileContent);
            }
            //-----------End of Prepare Folder
        }
    }
}
