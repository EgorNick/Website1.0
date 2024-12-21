using FluentEmail.Core;
using itProgerWebSite.Models;
using Microsoft.AspNetCore.Mvc;

namespace itProgerWebSite.Controllers
{
    public class ContactsController : Controller
    {
        public IActionResult Index()
        {
            return View(new Contact());
        }

        [HttpPost]
public async Task<IActionResult> SendEmail(Contact model)
{
    if (!ModelState.IsValid)
    {
        return View("Index", model);
    }

    try
    {
        var email = await Email
            .From(model.Email) 
            .To(model.RecipientEmail) 
            .Subject(model.Subject) 
            .Body($"От: {model.Name} {model.Surname}\n\n{model.Body}") 
            .SendAsync();

        if (email.Successful)
        {
            TempData["SuccessMessage"] = "Письмо успешно отправлено!";
        }
        else
        {
            TempData["ErrorMessage"] = "Ошибка при отправке письма.";
        }
    }
    catch (Exception ex)
    {
        var errorPath = Path.GetTempPath();
        TempData["ErrorMessage"] = $"Ошибка: {ex.Message}. Путь: {errorPath}";
    }

    return RedirectToAction("Index");
}
    }
}