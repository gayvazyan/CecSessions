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
    public class CreateModel : PageModel
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public CreateModel(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
            Input = new InputModel();
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            public string RoleName { get; set; }

        }

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
                    var roles = _roleManager.Roles;

                    if (roles.FirstOrDefault(p => p.Name == Input.RoleName) == null)
                    {
                        // We just need to specify a unique role name to create a new role
                        var identityRole = new IdentityRole
                        {
                            Name = Input.RoleName
                        };
                        // Saves the role in the underlying AspNetRoles table
                        var result = await _roleManager.CreateAsync(identityRole);
                        return RedirectToPage("/Admin/Roles/Index");
                    }
                    else
                    {
                        Errors.Add(new ServiceError { Code = "Սխալ", Description = "Տվյալ անվանումով դեր գոյություն ունի։" });
                    }
                   
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
