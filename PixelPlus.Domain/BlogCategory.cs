using PixelPlus.Domain.Blog;
using PixelPlus.Domain.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixelPlus.Domain
{
    public class BlogCategory
    {
        public BlogCategory(Guid blogAggregateId, Guid categoryAggregateId)
        {
            BlogAggregateId = blogAggregateId;
            CategoryAggregateId = categoryAggregateId;
        }

        public Guid BlogAggregateId { get; set; }
        public virtual BlogAggregate Blog { get; set; }
        public Guid CategoryAggregateId { get; set; }
        public virtual CategoryAggregate Category { get; set; }
    }
}
