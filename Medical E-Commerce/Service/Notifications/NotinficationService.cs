
using Medical_E_Commerce.Helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Medical_E_Commerce.Service.Notifications;

public class NotinficationService(
    ApplicationDbcontext dbcontext,
    UserManager<ApplicationUser> userManager ,
    IHttpContextAccessor httpContextAccessor,
    IEmailSender emailSender) : INotinficationService
{
    private readonly ApplicationDbcontext dbcontext = dbcontext;
    private readonly UserManager<ApplicationUser> userManager = userManager;
    private readonly IHttpContextAccessor httpContextAccessor = httpContextAccessor;
    private readonly IEmailSender emailSender = emailSender;

    
       public async Task SendPharmacyNotification()
        {
        IEnumerable<Entities.Pharmacy> Pharmacy = [];


        Pharmacy = await dbcontext.Pharmacies
                .Where(x => x.Items.Any())
                .AsNoTracking()
                .Take(3)
                .ToListAsync();
        

        //TODO: Select members only
        var users = await userManager.Users.ToListAsync();

        var origin = httpContextAccessor.HttpContext?.Request.Headers.Origin;

        foreach (var Pharmacyi in Pharmacy)
        {
            foreach (var user in users)
            {
                var placeholders = new Dictionary<string, string>
                {
                    { "{{name}}", user.UserFullName },
                    { "{{pollTill}}", Pharmacyi.Name},
                    { "{{endDate}}",$"{DateTime.UtcNow.AddDays(3)} "},
                    { "{{url}}", $"{origin}/pharmacy/start/{Pharmacyi.Id}" }
                };

                var body = EmailBodyBuilder.GenerateEmailBody("Care.Capsole.Notification", placeholders);

                await emailSender.SendEmailAsync(user.Email!, $"📣 Care-Capsole: Pharmacy - {Pharmacyi.Name}", body);
            }
        }
    }
}
