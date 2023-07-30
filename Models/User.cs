using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BlogAppMy.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        
        public int Age { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public List<Article> Articles { get; set; }
    
    }
}
