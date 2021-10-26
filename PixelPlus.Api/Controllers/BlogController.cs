using MediatR;
using Microsoft.AspNetCore.Mvc;
using PixelPlus.Api.Models.Blog.Requests;
using PixelPlus.Application.Blog.Commands;
using PixelPlus.Application.Blog.Queries;
using PixelPlus.Domain.Blog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PixelPlus.Api.Controllers
{
    [Route("blogs")]
    public class BlogController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BlogController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{blogId}")]
        public async Task<BlogAggregate> GetBlog([FromRoute] Guid blogId)
        {
            return await _mediator.Send(new BlogByIdQuery(blogId));
        }

        [HttpGet]
        public async Task<List<BlogAggregate>> Get([FromQuery] Guid? categoryId = null)
        {
            return await _mediator.Send(new BlogsByCategoryIdQuery(categoryId));
        }

        [HttpPost]
        [ProducesResponseType(typeof(BlogAggregate), 201)]
        public async Task<IActionResult> Create([FromBody] CreateBlogCommand command)
        {
            BlogAggregate blog = await _mediator.Send(command);
            return CreatedAtAction("Create", blog);
        }

        [HttpPut("{blogId}")]
        [ProducesResponseType(204)]
        public async Task<IActionResult> Update([FromRoute] Guid blogId, [FromBody] UpdateBlogRequest request)
        {
            await _mediator.Send(request.ToCommand(blogId));
            return NoContent();
        }

        [HttpDelete("{blogId}")]
        [ProducesResponseType(204)]
        public async Task<IActionResult> Delete([FromRoute] Guid blogId)
        {
            await _mediator.Send(new RemoveBlogCommand(blogId));
            return NoContent();
        }

        [HttpPost("{blogId}/categories")]
        [ProducesResponseType(204)]
        public async Task<IActionResult> AddCategory([FromRoute] Guid blogId, [FromBody] AddCategoryToBlogRequest request)
        {
            await _mediator.Send(request.ToCommand(blogId));
            return NoContent();
        }

        [HttpDelete("{blogId}/categories")]
        [ProducesResponseType(204)]
        public async Task<IActionResult> RemoveCategory([FromRoute] Guid blogId, [FromBody] RemoveCategoryFromBlogRequest request)
        {
            await _mediator.Send(request.ToCommand(blogId));
            return NoContent();
        }
    }
}
