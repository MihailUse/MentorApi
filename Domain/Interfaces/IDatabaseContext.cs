using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Domain.Interfaces;

public interface IDatabaseContext
{
    public DbSet<User> Users { get; }
    public DbSet<Settings> Settings { get; }
    public DbSet<Profile> Profiles { get; }
    public DbSet<Chat> Chats { get; }
    public DbSet<Message> Messages { get; }
    public DbSet<TrainingCourse> TrainingCourses { get; }
    public DbSet<Attach> Attaches { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = new());
}