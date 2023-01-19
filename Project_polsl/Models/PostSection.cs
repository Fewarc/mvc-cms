namespace Project_polsl.Models;

public class PostSection
{
    public enum SectionType
    {
        Text,
        Image,
        TextImage,
        ImageText
    }

    public PostSection(SectionType type, string content, string imagePath)
    {
        Type = type;
        Content = content;
        ImagePath = imagePath;
    }

    public int Id { set; get; }
    public int PostId { set; get; }
    public SectionType Type { set; get; }
    public string Content { set; get; }
    public string ImagePath { set; get; }
}