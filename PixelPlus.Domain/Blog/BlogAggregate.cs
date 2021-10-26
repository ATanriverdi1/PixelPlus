using System.Collections.Generic;

namespace PixelPlus.Domain.Blog
{
    public class BlogAggregate : BaseRoot
    {
        public BlogAggregate() { }
        public BlogAggregate(string title, string content)
        {
            Title = title;
            Content = content;
            Categories = new();
            SetAsCreated();
        }

        public string Title { get; set; }
        public string Content { get; set; }
        public virtual List<BlogCategory> Categories { get; set; }

        public void Update(string title, string content)
        {
            Title = title;
            Content = content;
            SetAsModified();
        }
    }
}
