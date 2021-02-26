using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using CecSessions.Core;
using CecSessions.Core.Models.Json;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CecSessions.UI.Pages.Client.Json
{
    public class CreateModel : PageModel
    {

        private readonly IWebHostEnvironment _env;
        public CreateModel(IWebHostEnvironment env)
        {
            _env = env;
            Create = new CreateJsonModel();
            ResultDataJsonList = new List<ResultDataJson>();
        }

        public string ResultFilePath { get; set; }
        private JsonSerializerOptions jsonOptions { get; set; }


        [BindProperty]
        public CreateJsonModel Create { get; set; }

        public class CreateJsonModel : ResultDataJson { }

        public List<ResultDataJson> ResultDataJsonList { get; set; }


        private List<ServiceError> _errors;
        public List<ServiceError> Errors
        {
            get => _errors ?? (_errors = new List<ServiceError>());
            set => _errors = value;
        }

        public void OnGet()
        {
        }

        public ActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                try
                {
                    

                    var resultDir = Path.Combine(_env.WebRootPath, "json");
                    if (!Directory.Exists(resultDir))
                    {
                        DirectoryInfo di = Directory.CreateDirectory(resultDir);
                    }
                    var resultFileName = "result.json";
                    string resultFilePath = Path.Combine(resultDir, resultFileName);
                    ResultFilePath = "/json/" + resultFileName;


                   


                    if (System.IO.File.Exists(resultFilePath))
                    {

                            var fileContent = System.IO.File.ReadAllText(resultFilePath);
                            List<ResultDataJson> fileJSONList = Newtonsoft.Json.JsonConvert.DeserializeObject<List<ResultDataJson>>(fileContent);

                            foreach (var item in fileJSONList)
                            {
                               ResultDataJsonList.Add(item);
                            }
                            ResultDataJsonList.Add(new ResultDataJson { Id= ResultDataJsonList.Count +1, Code = Create.Code, Data = Create.Data });
                    }



                    // Create Json file
                    if (System.IO.File.Exists(resultFilePath))
                    {
                        System.IO.File.Delete(resultFilePath);
                    }

                    System.IO.File.WriteAllText(resultFilePath, JsonSerializer.Serialize(ResultDataJsonList, jsonOptions));


                    return RedirectToPage("/Client/Json/Index");

                }
                    
                    catch (Exception ex)
                {
                    Errors.Add(new ServiceError { Code = "001", Description = ex.Message });
                }
            }
            return Page();
        }
    }

}

