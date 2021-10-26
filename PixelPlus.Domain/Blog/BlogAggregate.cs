using PixelPlus.Domain.Blog.Enum;
using PixelPlus.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PixelPlus.Domain.Blog
{
    public class BlogAggregate : BaseRoot
    {
        public BlogAggregate() { }
        public BlogAggregate(string title, string content)
        {
            Title = title;
            Content = content;
            SetAsCreated();
        }

        public string Title { get; set; }
        public string Content { get; set; }

        public virtual List<BlogCategoryRecord> Categories { get; set; }

        public BlogCategoryRecord AddCategory(Guid categoryId)
        {
            if (Categories.Any(p => p.CategoryId == categoryId && p.BlogId == Id))
                throw new BusinessException(BlogException.ThereIsAlreadyACategoryExists, 
                    new KeyValuePair<string, string>("Id", categoryId.ToString()));

            var category = new BlogCategoryRecord(Id, categoryId);
            Categories.Add(category);
            SetAsModified();
            return category;
        }

        public void RemoveCategory(Guid categoryId)
        {
            var category = Categories.FirstOrDefault(p => p.CategoryId == categoryId);

            if (category == null) return;

            Categories.Remove(category);
            SetAsModified();
        }
    }
}
