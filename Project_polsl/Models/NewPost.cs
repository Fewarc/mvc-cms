using System.Text.Json;

namespace Project_polsl.Models;

public class NewPost
{
    public Post PostData { set; get; }
    public PostSection[] PostSections { set; get; }

    public NewPost()
    {
        PostData = new Post();
        PostSections = new PostSection[]{};
    }

    public PostSection[] GetSections()
    {
        return PostSections;
    }

    public string GetTitle()
    {
        return PostData.Title;
    }
    
    public NewPost AddSection(PostSection.SectionType type, string content, string imagePath)
    {
        var newSection = new PostSection(type, content, imagePath);
        
        var sectionsList = PostSections.ToList();
        
        sectionsList.Add(newSection);

        PostSections = sectionsList.ToArray();
        
        return this;
    }

    public NewPost DeleteSection(int index)
    {
        var sectionList = PostSections.ToList();

        sectionList.RemoveAt(index);

        PostSections = sectionList.ToArray();
        
        return this;
    }

    public NewPost UpdateSection(int index, string text)
    {
        var sectionList = PostSections.ToList();

        var section = sectionList.ElementAt(index);

        if (section.Type == PostSection.SectionType.Text)
        {
            section.Content = text;
        }
        else
        {
            section.ImagePath = text;
        }
        
        PostSections = sectionList.ToArray();
        
        return this;
    }
    
    public NewPost UpdatePostData(string title, string createdAt)
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

    public Boolean IsValid()
    {
        if (PostSections.Length == 0)
        {
            return false;
        }
        
        if (string.IsNullOrEmpty(PostData.Title) || string.IsNullOrEmpty(PostData.Thumbnail))
        {
            return false;
        }
        
        foreach (var postSection in PostSections)
        {
            var content = postSection.Type == PostSection.SectionType.Text
                ? postSection.Content
                : postSection.ImagePath;

            if (string.IsNullOrEmpty(content))
            {
                return false;
            }
        }
        
        return true;
    }
}