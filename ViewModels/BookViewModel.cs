using ExamMvc.Models;
using System.Collections.Generic;

namespace ExamMvc.ViewModels
{
    public class BookViewModel
    {
        public IEnumerable<Book> Books { get; set; }
    }
}
