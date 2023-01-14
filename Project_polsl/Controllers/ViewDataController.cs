using Microsoft.AspNetCore.Mvc;

namespace Project_polsl.Controllers;

public class ViewDataController : Controller
{
    private readonly DataContext _context;

    public ViewDataController(DataContext context)
    {
        _context = context;
    }

    // GET
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult ViewUsers()
    {
        var users = _context.Users.ToList();
        
        return View(users);
    }

    public IActionResult AddUser()
    {
        return View();
    }
}