using EventRegistrationAPI_AndrewDo.Models;
using Microsoft.EntityFrameworkCore;

namespace EventRegistrationAPI_AndrewDo.Data
{
    public class AppDbContext : DbContext
    {
        // Constructor with options parameter
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Event> Events { get; set; }
        public DbSet<Registration> Registrations { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Event>().HasData(
                // Past event (already happened)
                new Event
                {
                    EventId = 1,
                    Title = "First day of school",
                    Location = "Student Centre",
                    EventDate = new DateTime(2025, 9, 10, 18, 0, 0),
                    Capacity = 100
                },
                // Future event with spots left
                new Event
                {
                    EventId = 2,
                    Title = "Career fair",
                    Location = "school gym",
                    EventDate = new DateTime(2027, 2, 15, 10, 0, 0),
                    Capacity = 50
                },
                // Full event: capacity 2 and 2 registrations below
                new Event
                {
                    EventId = 3,
                    Title = "Robotics club",
                    Location = "Room B204",
                    EventDate = new DateTime(2027, 3, 5, 14, 0, 0),
                    Capacity = 2
                },
                // Another future event with spots left
                new Event
                {
                    EventId = 4,
                    Title = "Hackathon",
                    Location = "Library",
                    EventDate = new DateTime(2027, 4, 10, 9, 0, 0),
                    Capacity = 30
                }
            );
            
            modelBuilder.Entity<Registration>().HasData(
                new Registration
                {
                    RegistrationId = 1,
                    EventId = 3,
                    StudentName = "Andrew Do",
                    Email = "andrew.do@gmail.com",
                    RegisteredOn = new DateTime(2026, 11, 1, 9, 0, 0)
                },
                new Registration
                {
                    RegistrationId = 2,
                    EventId = 3,
                    StudentName = "bob tom",
                    Email = "bob.tom@gmail.com",
                    RegisteredOn = new DateTime(2026, 11, 2, 10, 30, 0)
                }
            );
        }
    }
}