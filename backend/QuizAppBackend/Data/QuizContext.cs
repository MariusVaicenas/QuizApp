using Microsoft.EntityFrameworkCore;
using QuizAppBackend.Models;

namespace QuizAppBackend.Data
{
    public class QuizContext : DbContext
    {
        public QuizContext(DbContextOptions<QuizContext> options) : base(options) { }

        public DbSet<Question> Questions { get; set; }
        public DbSet<Option> Options { get; set; }
        public DbSet<QuizEntry> QuizEntries { get; set; } 
    }
}



