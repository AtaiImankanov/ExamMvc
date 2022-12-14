using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ExamMvc.Models
{
    public class Take
    {
        public int Id { get; set; }
        [Required]
        [EmailAddress]
        [Remote(action:"IsExistsEmailNMore3",controller:"Takes",ErrorMessage ="There is no emails like that Or you cant get more than 3 books")]
        public string Email { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int BookId { get; set; }
        public Book Book { get; set; }
    }
}
