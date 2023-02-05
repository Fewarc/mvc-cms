using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project_polsl.Models;

namespace Project_polsl.Controllers;

public class PostController : Controller
{
    private readonly DataContext _context;

    public PostController(DataContext context)
    {
        _context = context;
    }

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
        var postSections = _context.PostSections.Where(section => section.PostId == id).OrderBy(section => section.Id).ToList();
        var post = _context.Posts.FirstOrDefault(post => post.Id == id);

        post.Sections = postSections;
        
        return View(post);
    }
    
    [Route("Post/AllPosts/{filter}")]
    public IActionResult AllPosts(string filter = "")
    {
        var posts = _context.Posts.OrderByDescending(post => post.CreationDate).ToList();

        if (filter.Length > 0 && filter != "?")
        {
            posts = posts.FindAll(post => post.Title.Contains(filter));
        }

        return View(posts);
    }

    public IActionResult EditPost(int id)
    {
        var post = _context.Posts.FirstOrDefault(post => post.Id == id);
        var postSections = _context.PostSections.Where(postSection => postSection.PostId == id).OrderBy(section => section.Id).ToArray();

        var editPost = new NewPost(post!, postSections);

        if (HttpContext.Session.GetString("NewPost") != null)
        {
            var savedPost = JsonSerializer.Deserialize<NewPost>(HttpContext.Session.GetString("NewPost"));
            
            if (savedPost.PostData.Id != id)
            {
                HttpContext.Session.SetString("NewPost", JsonSerializer.Serialize(editPost));
            }
        }
        else
        {
            HttpContext.Session.SetString("NewPost", JsonSerializer.Serialize(editPost));
        }
        
        return View();
    }

    public IActionResult SaveChanges()
    {
        if (HttpContext.Session.GetString("NewPost") != null)
        {
            NewPost savedPost = JsonSerializer.Deserialize<NewPost>(HttpContext.Session.GetString("NewPost"));
            Post record = _context.Posts.FirstOrDefault(post => post.Id == savedPost.PostData.Id);

            record.Title = savedPost.GetTitle();
            record.Thumbnail = savedPost.PostData.Thumbnail;
            record.EditDate = DateTime.Now.ToString();
            record.Sections = savedPost.PostSections;
            _context.Entry(record).State = EntityState.Modified;

            _context.SaveChanges();
            
            HttpContext.Session.Remove("NewPost");
        }

        return Redirect("/User/ToLogIn");
    }
}