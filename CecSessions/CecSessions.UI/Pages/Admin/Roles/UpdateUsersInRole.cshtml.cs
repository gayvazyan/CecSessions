using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CecSessions.Core;
using CecSessions.Core.Entities;
using CecSessions.Core.Models.Admin;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace CecSessions.UI.Pages.Admin.Roles
{
    public class UpdateUsersInRoleModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userService;
        private readonly RoleManager<IdentityRole> _roleService;
        public UpdateUsersInRoleModel(UserManager<ApplicationUser> userService,
                                      RoleManager<IdentityRole> roleService)
        {
            _userService = userService;
            _roleService = roleService;
            UserRoleList = new List<UserRole>();
        }

        public List<ApplicationUser> Users { get; set; }
        public IEnumerable<IdentityRole> Roles { get; set; }

        [BindProperty]
        public string RoleId { get; set; }

        [BindProperty]
        public UserRole UserRole { get; set; }

        [BindProperty]
        public List<UserRole> UserRoleList { get; set; }

        private List<ServiceError> _errors;
        public List<ServiceError> Errors
        {
            get => _errors ?? (_errors = new List<ServiceError>());
            set => _errors = value;
        }

        public async Task OnGetAsync(string id)
        {
           

            RoleId = id;
            var role = await _roleService.FindByIdAsync(RoleId);


            var users = await _userService.Users.ToListAsync();
            foreach (var user in users)
            {
                var userRole = new UserRole
                {
                    UserId = user.Id,
                    UserName = user.UserName
                };

                if (await _userService.IsInRoleAsync(user, role.Name))
                {
                    userRole.IsSelected = true;
                }
                else
                {
                    userRole.IsSelected = false;
                }

                UserRoleList.Add(userRole);
            }
        }

        public async Task<ActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                try
                {
                   var role = await _roleService.FindByIdAsync(RoleId);

                    for (int i = 0; i < UserRoleList.Count; i++)
                    {
                        var user = await _userService.FindByIdAsync(UserRoleList[i].UserId);

                        IdentityResult result = null;

                        if (UserRoleList[i].IsSelected && !(await _userService.IsInRoleAsync(user, role.Name)))
                        {
                            result = await _userService.AddToRoleAsync(user, role.Name);
                        }
                        else if (!UserRoleList[i].IsSelected && await _userService.IsInRoleAsync(user, role.Name))
                        {
                            result = await _userService.RemoveFromRoleAsync(user, role.Name);
                        }
                        else
                        {
                            continue;
                        }
                    }
                    return RedirectToPage("/Admin/Roles/Index");
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
