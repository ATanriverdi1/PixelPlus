using System.Collections.Generic;

namespace PixelPlus.Domain.Category
{
    public class CategoryAggregate : BaseRoot
    {
        public CategoryAggregate()
        {

        }
        public CategoryAggregate(string name)
        {
            Name = name;
            SetAsCreated();
        }

        public string Name { get; set; }

        public virtual List<BlogCategory> Blogs { get; set; }

        public void Update(string name)
        {
            Name = name;
            SetAsModified();
        }
    }
}
