using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CodingTest.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<ToDoModel> ToDoItems { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<ToDoModel>(entity =>
            {
                entity.Property(e => e.Title)
                .HasMaxLength(100);

                entity.Property(e => e.Description)
                .HasMaxLength(100);

                entity.Property(e => e.DueDate)
                .IsRequired();

                entity.Property(e => e.DayConstraint);
            });

            base.OnModelCreating(builder);
        }
    }
}