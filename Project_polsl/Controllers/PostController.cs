using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Project_polsl.Models;

namespace Project_polsl.Controllers;

public class PostController : Controller
{
    private readonly DataContext _context;

    public PostController(DataContext context)
    {
        _context = context;
    }

    // public IActionResult 
    public IActionResult AddPost(string text)
    {
        return RedirectToAction("AddPost", "ViewData");
    }

    public IActionResult Delete(string title, string createdAt)
    {
        _context.Remove(_context.Posts.Single(post => post.Title == title && post.CreationDate == createdAt));
        _context.SaveChanges();
        
        return Redirect("/ViewData/ViewPosts");
    }
    
    public IActionResult SavePost()
    {
        if (HttpContext.Session.GetString("NewPost") != null)
        {
            var newPost = JsonSerializer.Deserialize<NewPost>(HttpContext.Session.GetString("NewPost"));

            Post postToSave = newPost.PostData;

            postToSave.Sections = newPost.GetSections();

            try
            {
                _context.Posts.Add(postToSave);
                _context.SaveChanges();
                
                HttpContext.Session.Remove("NewPost");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        return Redirect("/User/ToLogIn");
    }

    public IActionResult ViewPost(int id)
    {
        var postSections = _context.PostSections.Where(section => section.PostId == id).ToList();
        var post = _context.Posts.FirstOrDefault(post => post.Id == id);

        post.Sections = postSections;
        
        return View(post);
    }

    public IActionResult AllPosts()
    {
        var posts = _context.Posts.ToList();
        
        return View(posts);
    }
}