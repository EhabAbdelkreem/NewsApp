using System;
using System.Collections.Generic;
using System.Text;

namespace NewsApp.Domain.Entities
{
    public class Category
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Slug { get; set; } = string.Empty;

        public ICollection<News> NewsItems { get; set; } = new List<News>();
    }
}