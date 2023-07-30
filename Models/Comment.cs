using System;

namespace BlogAppMy.Models
{
    public class Comment
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public Guid ArticleId { get; set; }
        public Article Article { get; set; }
    }
}
