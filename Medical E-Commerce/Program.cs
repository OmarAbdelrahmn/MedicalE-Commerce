using Medical_E_Commerce;
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


app.UseHttpsRedirection();
app.UseCors();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
