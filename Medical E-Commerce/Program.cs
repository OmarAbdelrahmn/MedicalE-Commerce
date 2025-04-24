using Medical_E_Commerce;
using Medical_E_Commerce.Service.Notifications;
using Serilog;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDependency(builder.Configuration);

builder.Host.UseSerilog((context, configration) =>
    configration
    .ReadFrom.Configuration(context.Configuration)

    );



var app = builder.Build();

// Configure the HTTP request pipeline.

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHangfireDashboard("/jobs");

var scopeFactory = app.Services.GetRequiredService<IServiceScopeFactory>();
using var scope = scopeFactory.CreateScope();
var notificationService = scope.ServiceProvider.GetRequiredService<INotinficationService>();

RecurringJob.AddOrUpdate("SendPharmacyNotification", () => notificationService.SendPharmacyNotification(), Cron.Weekly);


app.UseHttpsRedirection();

app.UseCors();

app.UseAuthentication();

app.UseAuthorization();

app.UseStaticFiles();

app.MapControllers();

app.Run();
