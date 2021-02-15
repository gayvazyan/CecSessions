using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CecSessions.Core;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CecSessions.UI.Pages.Admin.Roles
{
    public class DeleteModel : PageModel
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        public DeleteModel(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
            Delete = new IdentityRole();
        }
        [BindProperty]
        public IdentityRole Delete { get; set; }


        private List<ServiceError> _errors;
        public List<ServiceError> Errors
        {
            get => _errors ?? (_errors = new List<ServiceError>());
            set => _errors = value;
        }

        public async Task PrepareAsync(string id)
        {
            Delete = await _roleManager.FindByIdAsync(id);
        }

        public async Task OnGetAsync(string id)
        {
            await PrepareAsync(id);
        }

        public async Task<ActionResult> OnPostAsync()
        {

            if (ModelState.IsValid)
            {
                try
                {
                    var result = await _roleManager.DeleteAsync(Delete);

                    if (result.Succeeded)
                        return RedirectToPage("/Admin/Roles/Index");
                    else
                        await PrepareAsync(Delete.Id);
                }
                catch (Exception ex)
                {
                    Errors.Add(new ServiceError { Code = "005", Description = ex.Message });
                }
            }
            return Page();
        }
    }
}
