using PixelPlus.Domain.Blog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public virtual List<BlogCategoryRecord> Blogs { get; set; }
    }
}
