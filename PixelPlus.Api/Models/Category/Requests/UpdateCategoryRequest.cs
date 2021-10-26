using PixelPlus.Application.Category.Commands;
using System;

namespace PixelPlus.Api.Models.Category.Requests
{
    public class UpdateCategoryRequest
    {
        public string Name { get; set; }

        public UpdateCategoryCommand ToCommand(Guid categoryId)
        {
            return new(categoryId, Name);
        }
    }
}
