
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BlogAppMy.Models
{
    public class Article
    {
        public Guid Id { get; set; }
        [Required]
        [StringLength(30)]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
        public List<Tag>  Tag { get; set; }
        public List<Comment> Сomments { get; set; }
    }
}
