﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using CecSessions.Core.Entities;
using CecSessions.Core;

namespace PecMembers.UI.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class ForgotPasswordModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public ForgotPasswordModel(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            public string Email { get; set; }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(Input.Email);
                if (user == null || !(await _userManager.IsEmailConfirmedAsync(user)))
                {
                    // Don't reveal that the user does not exist or is not confirmed
                    return RedirectToPage("./ForgotPasswordConfirmation");
                }

                // For more information on how to enable account confirmation and password reset please 
                // visit https://go.microsoft.com/fwlink/?LinkID=532713
                var code = await _userManager.GeneratePasswordResetTokenAsync(user);
                code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                var callbackUrl = Url.Page(
                    "/Account/ResetPassword",
                    pageHandler: null,
                    values: new { area = "Identity", code },
                    protocol: Request.Scheme);


                var toMail = Input.Email;
                var subject = " Հարգելի Email";
                var text = new StringBuilder();
                //text.AppendFormat("Հարգելի օգտատեր , {0}\n", user);
                text.AppendFormat("Հարգելի օգտատեր");
               // text.AppendLine(callbackUrl.ToString());
                text.AppendLine($"ՏԸՀ անդամներ համակարգում ձեր գաղտնաբառը փոխելու համար  անցեք հետևյալ ՝ <a href='{callbackUrl}'>հղմամբ</a>.");
                text.AppendLine("Եթե Դուք չէք ցանկանում փոխել Ձեր գախտնաբառը կամ գրանցված չէք վերը նշված համակարգում, պարզապես անտեսեք այս հաղորդագրությունը");
                text.AppendLine("Հարգանքներով Գարո");
                var textForMail = text.ToString();


                MailSender.Sender(toMail, subject, textForMail);


                //await _emailSender.SendEmailAsync(
                //    Input.Email,
                //    "Reset Password",
                //    $"Please reset your password by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                return RedirectToPage("./ForgotPasswordConfirmation");
            }

            return Page();
        }
    }
}
