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
        private readonly IProcedureTest _procedureTest;
        public IndexModel(IWebHostEnvironment env, IProcedureTest procedureTest)
        {
            _env = env;
            _procedureTest = procedureTest;
            JsonList = new List<ResultDataJson>();
        }

       

        [BindProperty]
        public List<ResultDataJson> JsonList { get; set; }


        public void OnGet()
        {
            //-----------Start of Prepare Folder
            var uploadDir = Path.Combine(_env.WebRootPath, "json");
            var fileName = "data.json";
            string filePath = Path.Combine(uploadDir, fileName);
            if (System.IO.File.Exists(filePath))
            {
                var fileContent = System.IO.File.ReadAllText(filePath);
                JsonList = Newtonsoft.Json.JsonConvert.DeserializeObject<List<ResultDataJson>>(fileContent);
            }

            //-----------End of Prepare Folder



            //-----------Start of Prepare Result

            List<ResultDataJson> zoneData = new List<ResultDataJson>();
            var zoneResult = _chartCommonService.ChartCandidateResult(electionId, "zone");
            var zoneCodeList = zoneResult.Select(e => e.Code).Distinct().ToList();
            foreach (var code in zoneCodeList)
            {
                var zoneDataList = zoneResult.Where(p => p.Code == code).ToList();
                zoneData.Add(new ResultDataJson { Code = code, Data = zoneDataList });
            }
            resultDataJson.Add(new ResultDataJson { Code = "zone", Data = zoneData });

            //-----------End of Prepare ResultZone
        }
    }
}
