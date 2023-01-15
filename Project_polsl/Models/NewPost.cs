namespace Project_polsl.Models;

public class NewPost
{
    public Post PostData { set; get; }
    public PostSection[] PostSections { set; get; }

    public NewPost addSection(PostSection.SectionType type, string content, string imagePath)
    {
        var newSection = new PostSection()
        {
            Type = type,
            Content = content,
            ImagePath = imagePath
        };
        
        PostSections.Append(newSection);
        
        return this;
    }

    public NewPost updatePostData(string title, string createdAt)
    {
        PostData.Title = title;
        PostData.CreationDate = createdAt;
        
        return this;
    }

    public NewPost UpdatePostSectionPostIds(int id)
    {
        foreach (var postSection in PostSections)
        {
            postSection.Id = id;
        }

        return this;
    }
}