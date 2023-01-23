using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Project_polsl.Models;

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
    
    public IActionResult AddPost()
    {
        if (HttpContext.Session.GetString("NewPost") != null)
        {
            var post = JsonSerializer.Deserialize<NewPost>(HttpContext.Session.GetString("NewPost"));
            
            if (post.PostData.Id != 0)
            {
                HttpContext.Session.Remove("NewPost");
                HttpContext.Session.SetString("NewPost", JsonSerializer.Serialize(new NewPost()));
            }
        }
        else
        {
            HttpContext.Session.SetString("NewPost", JsonSerializer.Serialize(new NewPost()));
        }
        
        return View();
    }
    
    public void UpdateTitle(string text)
    {
        var newPost = JsonSerializer.Deserialize<NewPost>(HttpContext.Session.GetString("NewPost"));

        newPost.UpdatePostData(text, DateTime.Now.ToString());
        
        HttpContext.Session.SetString("NewPost", JsonSerializer.Serialize(newPost));
    }

    [Route("ViewData/AddSection/{sectionType}")]
    public IActionResult AddSection(string sectionType, string redirect)
    {
        if (HttpContext.Session.GetString("NewPost") != null)
        {
            var newPost = JsonSerializer.Deserialize<NewPost>(HttpContext.Session.GetString("NewPost"));

            if (sectionType == "text")
            {
                newPost.AddSection(PostSection.SectionType.Text, "", "");
            }
            else
            {
                newPost.AddSection(PostSection.SectionType.Image, "", "");
            }
            
            HttpContext.Session.SetString("NewPost", JsonSerializer.Serialize(newPost));
        }

        return Redirect(redirect);
    }

    [Route("ViewData/DeleteSection/{index:int}")]
    public IActionResult DeleteSection(int index, string redirect)
    {
        if (HttpContext.Session.GetString("NewPost") != null)
        {
            var newPost = JsonSerializer.Deserialize<NewPost>(HttpContext.Session.GetString("NewPost"));

            newPost.DeleteSection(index);
            
            HttpContext.Session.SetString("NewPost", JsonSerializer.Serialize(newPost));
        }
        
        return Redirect(redirect);
    }

    public void UpdateSection(string text, int id)
    {
        if (HttpContext.Session.GetString("NewPost") != null)
        {
            var newPost = JsonSerializer.Deserialize<NewPost>(HttpContext.Session.GetString("NewPost"));

            newPost.UpdateSection(id, text);
            
            HttpContext.Session.SetString("NewPost", JsonSerializer.Serialize(newPost));
        }
    }

    private const string ROOT_ROUTE = "C:/Users/berez/Desktop/src/mvc-cms/Project_polsl/wwwroot/uploaded/";
    private async Task<string> saveImage(IFormFile file)
    {
        string imageName = Path.GetFileName(file.FileName);
        string physicalPath = Path.Combine(ROOT_ROUTE + imageName);

        await using (Stream fileStream = new FileStream(physicalPath, FileMode.Create)) {
            await file.CopyToAsync(fileStream);
        }

        return imageName;
    }

    private void deleteImage(string imagePath)
    {
        if (System.IO.File.Exists(Path.Combine(imagePath)))
        {
            System.IO.File.Delete(Path.Combine(imagePath));
        }
    }

    [Route("ViewData/UpdateImageSection")]
    public void UpdateImageSection(IFormFile file, string id)
    {
        if (file == null)
        {
            Console.WriteLine("No file: ");
            Console.WriteLine(file);
        }
        string physicalPath = Path.GetFileName(file.FileName);
        saveImage(file);
        
        if (HttpContext.Session.GetString("NewPost") != null)
        {
            var newPost = JsonSerializer.Deserialize<NewPost>(HttpContext.Session.GetString("NewPost"));

            if (id == "newPostThumbnail")
            {
                if (newPost.PostData.Thumbnail != null && newPost.PostData.Thumbnail.Length > 0)
                {
                    deleteImage(newPost.PostData.Thumbnail);
                }

                newPost.PostData.Thumbnail = physicalPath;
            }
            else
            {
                var sectionId = int.Parse(id);
                var postSection = newPost.GetSections()[sectionId];
                
                if (postSection != null && postSection.ImagePath.Length > 0)
                {
                    deleteImage(postSection.ImagePath);
                }
                
                newPost.UpdateSection(sectionId, physicalPath);
            }

            HttpContext.Session.SetString("NewPost", JsonSerializer.Serialize(newPost));
        }
    }

    public IActionResult ViewPosts()
    {
        var posts = _context.Posts.ToList();
        return View(posts);
    }
}