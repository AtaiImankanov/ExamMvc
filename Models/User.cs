using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ExamMvc.Models
{
    public class User
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Surname { get; set; }
        [Required]
        [EmailAddress]
        [Remote(action: "IsEx", controller: "Users", ErrorMessage = "Already exists")]
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

    }
}
