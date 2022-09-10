using System;
using System.ComponentModel.DataAnnotations;

namespace ExamMvc.Models
{
    public class Book
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string AuthorName { get; set; }
        [Required]
        public string Pic { get; set; }
        public string YearOfIssue { get; set; }
        public string Description { get; set; }
        public DateTime DateAdded { get; set; }
    }
}
