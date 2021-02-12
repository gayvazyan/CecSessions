using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CecSessions.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace CecSessions.UI.Pages.Admin.Users
{
    public class IndexModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userService;

        public IndexModel(UserManager<ApplicationUser> userService)
        {
            _userService = userService;
            InputList = new List<InputModel>();
        }

        //[BindProperty]
        //public InputModel Input { get; set; }
        public List<InputModel> InputList { get; set; }
        public class InputModel
        {
            public string Id { get; set; }
            public string UserName { get; set; }
            public string Email { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
        }

        public List<ApplicationUser> Users { get; set; }


        public async Task OnGetAsync()
        {
            Users = await _userService.Users.ToListAsync();

            if (Users != null)
            {
                foreach (var item in Users)
                {
                    var Input = new InputModel()
                    {
                        Id = item.Id,
                        Email = item.Email,
                        UserName = item.UserName,
                        FirstName = item.UName,
                        LastName = item.PName
                    };
                    InputList.Add(Input);
                }
            }
        }
    }
}
