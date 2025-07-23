using Microsoft.AspNetCore.Mvc;
using FlexTask.Data;
using FlexTask.Models;

namespace FlexTask.Controllers
{
   public class ContactsController : Controller
{
    private readonly AppDbContext _db;
    public ContactsController(AppDbContext db) => _db = db;

    [HttpGet]
    public IActionResult Contact() => View();

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Contact(Contact model)
    {
        if (!ModelState.IsValid)
            return View(model);

        _db.Contacts.Add(model);
        await _db.SaveChangesAsync();

        return RedirectToAction(nameof(ThankYou));
    }

    [HttpGet]
    public IActionResult ThankYou() => View();
}

    }

