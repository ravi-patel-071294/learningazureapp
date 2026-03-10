using Microsoft.EntityFrameworkCore;
using ToDoApp.Models;

namespace ToDoApp.Data
{
    public class ToDoDbContext : DbContext
    {
        public ToDoDbContext(DbContextOptions<ToDoDbContext> options) : base(options)
        {
            
        }

        // DbSet representing ToDoItems table in database
        public DbSet<ToDoItem> ToDoItems { get; set; }
    }
}
