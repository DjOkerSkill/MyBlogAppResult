using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BlogAppMy.Models
{
    public class User
    {
        public Guid Id { get; set; }
        [Required]
        [StringLength(20)]
        public string Name { get; set; }
        [Required]
        [StringLength(20)]
        public string LastName { get; set; }
        [Required]
        [StringLength(2)]
        public int Age { get; set; }

        [Required]
        [StringLength(10)]
        public string Login { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 6)]
        public string Password { get; set; }
        public List<Article> Articles { get; set; }
    
    }
}
