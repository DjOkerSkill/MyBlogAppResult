using System;
using System.ComponentModel.DataAnnotations;

namespace BlogAppMy.Models
{
    public class Tag
    {
        public Guid Id { get; set; }
        [Required]
        [StringLength(30)]
        public string Name { get; set; }
        public Guid ArticleId { get; set; }
        public Article Article  { get; set; }
    }
}
