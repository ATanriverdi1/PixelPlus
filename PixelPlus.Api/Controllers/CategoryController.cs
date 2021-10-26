using MediatR;
using Microsoft.AspNetCore.Mvc;
using PixelPlus.Api.Models.Category.Requests;
using PixelPlus.Application.Category.Commands;
using PixelPlus.Application.Category.Queries;
using PixelPlus.Domain.Category;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PixelPlus.Api.Controllers
{
    [Route("categories")]
    public class CategoryController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CategoryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<List<CategoryAggregate>> Get()
        {
            return await _mediator.Send(new CategoryQuery());
        }

        [HttpPost]
        [ProducesResponseType(typeof(CategoryAggregate), 201)]
        public async Task<IActionResult> Create([FromBody] CreateCategoryCommand command)
        {
            var category = await _mediator.Send(command);
            return CreatedAtAction("Create", category);
        }

        [HttpPut("{categoryId}")]
        [ProducesResponseType(204)]
        public async Task<IActionResult> Update([FromRoute] Guid categoryId, [FromBody] UpdateCategoryRequest request)
        {
            await _mediator.Send(request.ToCommand(categoryId));
            return NoContent();
        }

        [HttpDelete("{categoryId}")]
        [ProducesResponseType(204)]
        public async Task<IActionResult> Delete([FromRoute] Guid categoryId)
        {
            await _mediator.Send(new RemoveCategoryCommand(categoryId));
            return NoContent();
        }
    }
}
