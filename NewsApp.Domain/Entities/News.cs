using System;
using System.Collections.Generic;
using System.Text;

namespace NewsApp.Domain.Entities
{
    public class News
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public string? ImageUrl { get; set; }
        public string? VideoUrl { get; set; }
        public DateTime PublishDate { get; set; } = DateTime.UtcNow;
        public bool IsBreaking { get; set; }
        public int Views { get; set; }

        public Guid CategoryId { get; set; }
        public Category Category { get; set; } = null!;
    }
}
