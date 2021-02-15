using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CecSessions.Core;
using CecSessions.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CecSessions.UI.Pages.Admin.Users
{
    public class DeleteModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userService;
        public DeleteModel(UserManager<ApplicationUser> userService)
        {
            _userService = userService;
            Delete = new ApplicationUser();
        }

        [BindProperty]
        public ApplicationUser Delete { get; set; }


        private List<ServiceError> _errors;
        public List<ServiceError> Errors
        {
            get => _errors ?? (_errors = new List<ServiceError>());
            set => _errors = value;
        }

        public async Task PrepareAsync(string id)
        {
            Delete = await _userService.FindByIdAsync(id);
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

                    var result = await _userService.DeleteAsync(Delete);

                    if (result.Succeeded)
                        return RedirectToPage("/Admin/Users/Index");
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
