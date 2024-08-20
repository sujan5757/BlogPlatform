using Microsoft.AspNetCore.Mvc;
using BlogPlatform.Models;
using System.Security.Cryptography;

namespace BlogPlatform.Controllers
{
    public class UserController : Controller
    {
        private readonly BlogDbContext _context;

        public UserController(BlogDbContext context)
        {
            _context = context;
        }

        // GET: User/Register
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        // POST: User/Register
    [HttpPost]
public IActionResult Register(User user)
{
    if (ModelState.IsValid)
    {
        var duplicate = _context.Users.SingleOrDefault(u => u.Email == user.Email);
        if (duplicate != null)
        {
            ViewBag.ErrorMessage = "User already exists. Please use a different email.";
            return View(user);
        }
        _context.Users.Add(user);
        _context.SaveChanges();
        TempData["UserID"] = user.Username;
        return RedirectToAction("Index", "Blog");
    }

    return View(user);
}
        [HttpGet]
        public IActionResult Login()
        {
            return View(); 
        } 

        [HttpPost]
      public IActionResult Login(User user)
    {
    if (ModelState.IsValid)
    {
        var login = _context.Users.SingleOrDefault(u => u.Email == user.Email);
        
        if (login != null && login.PasswordHash == user.PasswordHash && login.Username == user.Username)
        
        {
            TempData["UserID"] = login.Username;
            return RedirectToAction("Index", "Blog");
        }
        else
        {
            ViewBag.ErrorMessage = "Invalid login attempt. Please try again.";
            return View(user);
        }
    }
        return View(user); 

}


        
    }
}

