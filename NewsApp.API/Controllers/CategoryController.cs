using MediatR;
using Microsoft.AspNetCore.Mvc;
using NewsApp.Application.Queries;

namespace NewsApp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly IMediator _mediator;
        public CategoryController(IMediator mediator) => _mediator = mediator;

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _mediator.Send(new GetAllCategoriesQuery()));
    }
}