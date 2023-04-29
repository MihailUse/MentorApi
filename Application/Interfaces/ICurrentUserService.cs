using Domain.Entities;

namespace Application.Interfaces;

public interface ICurrentUserService
{
    public User User { get; set; }
}