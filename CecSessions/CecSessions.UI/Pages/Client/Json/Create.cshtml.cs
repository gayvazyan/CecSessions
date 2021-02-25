using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CecSessions.Core;
using CecSessions.Core.Models.Json;
using Microsoft.AspNetCore.Hosting;
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
        }

        public string ResultFilePath { get; set; }


        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel : ResultDataJson { }


        private List<ServiceError> _errors;
        public List<ServiceError> Errors
        {
            get => _errors ?? (_errors = new List<ServiceError>());
            set => _errors = value;
        }

        public void OnGet()
        {
        }

        public async Task<ActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                try
                {

                    var resultDir = Path.Combine(_env.WebRootPath, "json", (DateTime.Now.ToString("dd_MM_yyyy") + "_result"));
                    if (!Directory.Exists(resultDir))
                    {
                        DirectoryInfo di = Directory.CreateDirectory(resultDir);
                    }

                    var resultFileName = "result.json";
                    string resultFilePath = Path.Combine(resultDir, resultFileName);
                    ResultFilePath = "/json/" + (DateTime.Now.ToString("dd_MM_yyyy") + "_data") + "/" + resultFileName;


                    //    var roles = _roleManager.Roles;

                    //    if (roles.FirstOrDefault(p => p.Name == Input.RoleName) == null)
                    //    {
                    //        // We just need to specify a unique role name to create a new role
                    //        var identityRole = new IdentityRole
                    //        {
                    //            Name = Input.RoleName
                    //        };
                    //        // Saves the role in the underlying AspNetRoles table
                    //        var result = await _roleManager.CreateAsync(identityRole);
                    //        return RedirectToPage("/Admin/Roles/Index");
                    //    }
                    //    else
                    //    {
                    //        Errors.Add(new ServiceError { Code = "Սխալ", Description = "Տվյալ անվանումով դեր գոյություն ունի։" });
                    //    }

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

