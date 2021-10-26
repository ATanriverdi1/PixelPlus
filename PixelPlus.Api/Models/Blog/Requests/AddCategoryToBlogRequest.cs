using PixelPlus.Application.Blog.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PixelPlus.Api.Models.Blog.Requests
{
    public class AddCategoryToBlogRequest
    {
        public Guid CategoryId { get; set; }

        public AddCategoryToBlogCommand ToCommand(Guid blogId)
        {
            return new(blogId, CategoryId);
        }
    }
}
