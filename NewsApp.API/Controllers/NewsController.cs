using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NewsApp.API.Services;
using NewsApp.Application.Commands;
using NewsApp.Application.DTOs;
using NewsApp.Application.Queries;

namespace NewsApp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NewsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly FileService _fileService;

        public NewsController(IMediator mediator, FileService fileService)
        {
            _mediator = mediator;
            _fileService = fileService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _mediator.Send(new GetLatestNewsQuery()));

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var news = await _mediator.Send(new GetNewsByIdQuery(id));
            return news != null ? Ok(news) : NotFound();
        }

        [HttpPost]
        [Authorize] // يمكنك تحديد (Roles = "Admin") لاحقاً
        public async Task<IActionResult> Create([FromForm] string title, [FromForm] string content, [FromForm] bool isBreaking, [FromForm] Guid categoryId, IFormFile? image, IFormFile? video)
        {
            // حفظ الملفات إن وجدت
            var imageUrl = await _fileService.SaveFileAsync(image, "images");
            var videoUrl = await _fileService.SaveFileAsync(video, "videos");

            // إنشاء الـ DTO
            var dto = new CreateNewsDto
            {
                Title = title,
                Content = content,
                IsBreaking = isBreaking,
                CategoryId = categoryId,
                ImageUrl = imageUrl,
                VideoUrl = videoUrl
            };

            // إرسال الـ Command مع تمرير الـ DTO إليه (هذا ما كان ينقص!)
            var id = await _mediator.Send(new CreateNewsCommand(dto));

            return Ok(new { id });
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _mediator.Send(new DeleteNewsCommand(id));
            return result ? Ok() : NotFound();
        }
    }
}