using Microsoft.EntityFrameworkCore;
using Todo_List.Models;

namespace Todo_List.Data
{
    public class TodoAPIDbContext : DbContext
    {
        public TodoAPIDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Todo> Todos { get; set; }
    }
}
