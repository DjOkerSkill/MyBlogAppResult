using System;

namespace BlogAppMy.Models
{
    public class Tag
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid ArticleId { get; set; }
        public Article Article  { get; set; }
    }
}
