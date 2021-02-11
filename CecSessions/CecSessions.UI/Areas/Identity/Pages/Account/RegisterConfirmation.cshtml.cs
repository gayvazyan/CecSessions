using Microsoft.AspNetCore.Authorization;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using CecSessions.Core.Entities;
using CecSessions.Core;

namespace PecMembers.UI.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterConfirmationModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmailSender _sender;

        public RegisterConfirmationModel(UserManager<ApplicationUser> userManager, IEmailSender sender)
        {
            _userManager = userManager;
            _sender = sender;
        }

        public string Email { get; set; }

        public bool DisplayConfirmAccountLink { get; set; }

        public string EmailConfirmationUrl { get; set; }

        public async Task<IActionResult> OnGetAsync(string email)
        {
            if (email == null)
            {
                return RedirectToPage("/Index");
            }

            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return NotFound($"Unable to load user with email '{email}'.");
            }

            Email = email;
            // Once you add a real email sender, you should remove this code that lets you confirm the account
            DisplayConfirmAccountLink = true;
            if (DisplayConfirmAccountLink)
            {
                var userId = await _userManager.GetUserIdAsync(user);
                var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                EmailConfirmationUrl = Url.Page(
                    "/Account/ConfirmEmail",
                    pageHandler: null,
                    values: new { area = "Identity", userId = userId, code = code },
                    protocol: Request.Scheme);
            }



            //var SmtpServer = "smtp.gmail.com";
            //var SmtpPort = 587;
            //var SmtpUsername = "noreplaycec@gmail.com";
            //var SmtpPassword = "Sa123456!";
            var toMail = Email;
          //  var subject = " Հարգելի քաղաքացի";
            var text = new StringBuilder();
            //text.AppendFormat("Հարգելի օգտատեր , {0}\n", user);
            text.AppendLine("Ձեր Մուտքանուն է ՝"+user.UserName+"," );
            text.Append(System.Environment.NewLine);
            text.AppendLine(" գաղտնաբառն է՝ Sa123456! : Խնդրհում ենք հետագայում փոխել գաղտնաբառը։");
            //text.AppendLine(EmailConfirmationUrl.ToString());
            text.AppendLine($"ՏԸՀ անդամներ համակարգում ձեր գրանցումը հաջողությամբ ավարտելու համար անցեք հետևյալ ՝ <a href='{EmailConfirmationUrl}'>հղմամբ</a>.");
            text.AppendLine("Եթե Դուք չէք գրանցվել վերը նշված համակարգում, պարզապես անտեսեք այս հաղորդագրությունը");
            text.AppendLine("Հարգանքներով Գարո");
            var textForMail=text.ToString();


            //MailSender.Sender(toMail, subject, textForMail);



            return Page();
        }
    }
}
