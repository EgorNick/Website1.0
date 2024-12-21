using FluentEmail.Core;
using FluentEmail.Smtp;
using FluentEmail.Razor;
using System.Net.Mail;
using System.Net;
var tempPath = Path.GetTempPath();
Directory.SetCurrentDirectory(tempPath);

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddFluentEmail("e.nikulshin51@gmail.com")
    .AddRazorRenderer()
    .AddSmtpSender(new SmtpClient("smtp.gmail.com")
    {
        Port = 587,
        Credentials = new NetworkCredential("e.nikulshin51@gmail.com", "password"),
        EnableSsl = true,
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();