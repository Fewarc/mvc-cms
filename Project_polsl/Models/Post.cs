namespace Project_polsl.Models;

public class Post
{
    public int Id { set; get; }
    public string Title { set; get; }
    public string CreationDate { set; get; }

    public string Thumbnail { set; get; }

    public ICollection<PostSection> Sections { set; get; }
}