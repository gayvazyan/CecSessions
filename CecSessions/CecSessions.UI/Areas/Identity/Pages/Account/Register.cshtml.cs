﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using CecSessions.Core.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;

namespace PecMembers.UI.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;

        public RegisterModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public class InputModel
        {
            [Required(ErrorMessage = "էլ․հասցե մուտքագրված չէ")]
            [EmailAddress(ErrorMessage ="Մուտքագրեք վավեր էլ․հասցե")]
            [Display(Name = "Էլ․հասցե")]
            public string Email { get; set; }

            [Required(ErrorMessage = "Գաղտնաբառը մուտքագրված չէ")]
            [StringLength(100, ErrorMessage = " {0}ը պետք է կազմված լինի նվազագույնը {2}  առավելագույնը {1} նիշ։ ", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Գաղտնաբառ")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Կրկնել գաղտնաբառը")]
            [Compare("Password", ErrorMessage = "Գաղտնաբառը և կրկնված գաղտնաբառերը տարբեր են")]
            public string ConfirmPassword { get; set; }

            [Required(ErrorMessage = "Մուտքանունը մուտքագրված չէ")]
            [Display(Name = "Մուտքանուն")]
            public string UserNameInput { get; set; }

            [Required(ErrorMessage = "Անունը մուտքագրված չէ")]
            [Display(Name = "Անուն")]
            public string UNameInput { get; set; }

            [Required(ErrorMessage = "Ազգանունը մուտքագրված չէ")]
            [Display(Name = "Ազգանուն")]
            public string PNameInput { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            ViewData["emailCheck"] = string.Empty;
            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = Input.UserNameInput, Email = Input.Email ,UName=Input.UNameInput, PName=Input.PNameInput };
               
                var emailCheck = await _userManager.FindByEmailAsync(user.Email);

                if (emailCheck != null)
                {
                    ViewData["emailCheck"] = user.Email +" էլ. հասցեով համակարգում առկա է գրանցում";
                    return Page();
                }

                var result = await _userManager.CreateAsync(user, Input.Password);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");

                     var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { area = "Identity", userId = user.Id, code = code },
                        protocol: Request.Scheme);

                    await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                        $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        return RedirectToPage("RegisterConfirmation", new { email = Input.Email });
                    }
                    else
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        return LocalRedirect(returnUrl);
                    }
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
