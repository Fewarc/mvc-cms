using Microsoft.AspNetCore.Mvc;
using Project_polsl.Models;

namespace Project_polsl.Controllers;

public class UserController : Controller
{
    private readonly DataContext _context;


    public UserController(DataContext context)
    {
        _context = context;
    }

    // GET
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult ToLogIn()
    {
        return HttpContext.Session.GetString("Username") != null ? View("Dashboard") : View();
    }
    
    public IActionResult LogIn(string username, string password)
    {
        if (HttpContext.Session.GetString("Username") != null)
        {
            return View("Dashboard");
        }
        
        var user = _context.Users.FirstOrDefault(user => user.Username == username);

        if (user == null)
        {
            ViewData["LoginError"] = "User not found!";
            return View("ToLogIn");
        }
        
        if (user.Password == password)
        {
            HttpContext.Session.SetString("Username", user.Username);
            return View("Dashboard");
        }
        else
        {
            ViewData["LoginError"] = "Wrong password!";
            return View("ToLogIn");
        }
    }

    public IActionResult Delete(string username)
    {
        return RedirectToAction("ViewUsers", "ViewData");
    }
    
    public IActionResult Create(string username)
    {
        return View("Dashboard");
    }
}