using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Domain.Persistence;

public class DatabaseContext : DbContext, IDatabaseContext
{
    public DbSet<User> Users => Set<User>();
    public DbSet<Settings> Settings => Set<Settings>();
    public DbSet<Profile> Profiles => Set<Profile>();
    public DbSet<Chat> Chats => Set<Chat>();
    public DbSet<Message> Messages => Set<Message>();
    public DbSet<TrainingCourse> TrainingCourses => Set<TrainingCourse>();
    public DbSet<Attach> Attaches => Set<Attach>();

    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
    {
    }
}