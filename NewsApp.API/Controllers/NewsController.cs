using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NewsApp.Application.Commands;
using NewsApp.Application.Queries;

namespace NewsApp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NewsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public NewsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetLatestNews()
        {
            var news = await _mediator.Send(new GetLatestNewsQuery());
            return Ok(news);
        }

        [HttpPost]
        [Authorize] // يتطلب تسجيل الدخول
        public async Task<IActionResult> CreateNews([FromBody] CreateNewsCommand command)
        {
            var id = await _mediator.Send(command);
            return Ok(new { id });
        }
    }
}