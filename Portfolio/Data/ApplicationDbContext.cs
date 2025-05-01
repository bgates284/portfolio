using Microsoft.EntityFrameworkCore;
using Portfolio.Models;
using System.Collections.Generic;
using Portfolio.Models;

namespace Portfolio.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Mission> Missions { get; set; }
        public DbSet<Person> People { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Mission>().ToTable("Missions", "launchpad").HasKey("Id");
            //modelBuilder.Entity<Mission>().HasMany()

            modelBuilder.Entity<Mission>().Property(m => m.Title)
                .HasMaxLength(100)
                .IsRequired();

            

            modelBuilder.Entity<Person>(entity =>
            {
                entity.ToTable("People", "launchpad");
                entity.Property(p => p.Id).ValueGeneratedOnAdd();
            });
        }
    }
    public static class DbInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {
            context.Database.EnsureCreated();

            if (context.Missions.Any())
                return;
            
            context.People.Add(new Person { FirstName = "William", LastName = "Gates", DateOfBirth = DateTime.Now, Email = "wjg284@gmail.com", PhoneNumber = "252-417-3824" });

            context.SaveChanges();
            var person = context.People.FirstOrDefault();
            context.Missions.AddRange(
                new Mission { Title = "Explorer I", LaunchDate = DateTime.Now, Description = "Conqure", Owner = person},
                new Mission { Title = "Datanaut Probe", LaunchDate = DateTime.Now.AddDays(10), Description="Observe", Owner = person}
            );

            context.SaveChanges();
        }
    }
}