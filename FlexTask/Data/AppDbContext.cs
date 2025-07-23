using Microsoft.EntityFrameworkCore;
using FlexTask.Models;

namespace FlexTask.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<TodoTask> TodoTasks { get; set; }
        public DbSet<Contact> Contacts { get; set; }
    }
    
} 