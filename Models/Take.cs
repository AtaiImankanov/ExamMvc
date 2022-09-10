using System.ComponentModel.DataAnnotations;

namespace ExamMvc.Models
{
    public class Take
    {
        public int Id { get; set; }
        [Required]
        //remote
         public string Email { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int BookId { get; set; }
        public Book Book { get; set; }
    }
}
