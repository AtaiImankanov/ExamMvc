using Microsoft.EntityFrameworkCore;

namespace ExamMvc.Models
{
    public class MobileContext : DbContext
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<Take> Takes { get; set; }
        public DbSet<User> Users { get; set; }
        public MobileContext(DbContextOptions<MobileContext> options) : base(options)
        {

        }
    }
}
