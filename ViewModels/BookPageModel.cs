using ExamMvc.Models;
using System.Collections.Generic;

namespace ExamMvc.ViewModels
{
    public class BookPageModel
    {
        public IEnumerable<Book> Books { get; set; }
        public PageViewModel PageViewModel { get; set; }
    }
}
