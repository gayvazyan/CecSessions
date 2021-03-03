using System.Collections.Generic;
using System.IO;
using CecSessions.Core.Services.ProcedureTest;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CecSessions.UI.Pages.Admin.Json
{
    public class IndexModel : PageModel
    {
       
        private readonly IWebHostEnvironment _env;
        private readonly IProcedureTest _procedureTest;
        public IndexModel(IWebHostEnvironment env, IProcedureTest procedureTest)
        {
            _env = env;
            _procedureTest = procedureTest;
            JsonList = new List<ResultDataJson>();
        }

        public class ResultDataJson
        {
            public int Id { get; set; }
            public string Code { get; set; }
            public string Data { get; set; }
           
        }

        [BindProperty]
        public List<ResultDataJson> JsonList { get; set; }


        public void OnGet()
        {

            var uploadDir = Path.Combine(_env.WebRootPath, "json");
            var fileName = "data.json";
            string filePath = Path.Combine(uploadDir, fileName);
            if (System.IO.File.Exists(filePath))
            {
                var fileContent = System.IO.File.ReadAllText(filePath);
                JsonList = Newtonsoft.Json.JsonConvert.DeserializeObject<List<ResultDataJson>>(fileContent);
            }
        }
    }
}
