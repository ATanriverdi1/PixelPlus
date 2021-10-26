using PixelPlus.Domain.Category;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixelPlus.Domain.Blog
{
    public class BlogCategoryRecord
    {
        public BlogCategoryRecord() { }

        public BlogCategoryRecord(Guid blogId, Guid categoryId)
        {
            CategoryId = categoryId;
            BlogId = blogId;
        }

        public Guid CategoryId { get; set; }
        public virtual CategoryAggregate Category { get; set; }
        public Guid BlogId { get; set; }
        public virtual BlogAggregate Blog { get; set; }


    }
}
