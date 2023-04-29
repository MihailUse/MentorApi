namespace Domain.Entities;

public class TrainingCourse
{
    public Guid Id { get; set; }
    public string Title { get; set; } = null!;
    public string Body { get; set; } = null!;

    public Guid UserId { get; set; }
    public Guid AttachId { get; set; }

    public User User { get; set; } = null!;
    public Attach Attach { get; set; } = null!;
}