using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CecSessions.Core.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;

namespace PecMembers.UI.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class ResetPasswordModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public ResetPasswordModel(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required(ErrorMessage = "Էլ․հասեն մուտքագրված չէ")]
            [EmailAddress(ErrorMessage = "Մուտքագրեք վավեր էլ․հասցե")]
            [Display(Name = "Էլ․հասցե")]
            public string Email { get; set; }

            [Required(ErrorMessage = "Գաղտնաբառը մուտքագրված չէ")]
            [StringLength(100, ErrorMessage = "{0}ը պետք է ունենա  առնվազն {2} և առավելագույն {1} նիշ։", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Գաղտնաբառ")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Կրկնել գաղտնաբառը")]
            [Compare("Password", ErrorMessage = "Գաղտնաբառը և կրկնված գաղտնաբառերը տարբեր են")]
            public string ConfirmPassword { get; set; }

            public string Code { get; set; }
        }

        public IActionResult OnGet(string code = null)
        {
            if (code == null)
            {
                return BadRequest("A code must be supplied for password reset.");
            }
            else
            {
                Input = new InputModel
                {
                    Code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code))
                };
                return Page();
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = await _userManager.FindByEmailAsync(Input.Email);
            if (user == null)
            {
                // Don't reveal that the user does not exist
                return RedirectToPage("./ResetPasswordConfirmation");
            }

            var result = await _userManager.ResetPasswordAsync(user, Input.Code, Input.Password);
            if (result.Succeeded)
            {
                return RedirectToPage("./ResetPasswordConfirmation");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
            return Page();
        }
    }
}
