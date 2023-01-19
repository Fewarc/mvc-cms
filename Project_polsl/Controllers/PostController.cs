using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Project_polsl.Models;

namespace Project_polsl.Controllers;

public class PostController : Controller
{
    // public IActionResult 
    public IActionResult AddPost(string text)
    {
        HttpContext.Session.SetString("TestString", text);
        return RedirectToAction("AddPost", "ViewData");
    }
}