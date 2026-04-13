using System;
using System.Collections.Generic;
using System.Text;

namespace NewsApp.Application.DTOs
{
    public class NewsDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public string? ImageUrl { get; set; }
        public string? VideoUrl { get; set; }
        public DateTime PublishDate { get; set; }
        public bool IsBreaking { get; set; }
        public string CategoryName { get; set; } = string.Empty;
    }

    public class CreateNewsDto
    {
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public string? ImageUrl { get; set; }
        public string? VideoUrl { get; set; }
        public bool IsBreaking { get; set; }
        public Guid CategoryId { get; set; }
    }
}
