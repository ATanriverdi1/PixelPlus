using PixelPlus.Application.Blog.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PixelPlus.Api.Models.Blog.Requests
{
    public class UpdateBlogRequest
    {
        public string Title { get; set; }
        public string Content { get; set; }

        public UpdateBlogCommand ToCommand(Guid blogId)
        {
            return new(blogId, Title, Content);
        }
    }
}
