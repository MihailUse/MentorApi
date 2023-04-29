using Application.Interfaces;
using Domain.Entities;

namespace Application.Services;

public class CurrentUserService : ICurrentUserService
{
    public User User { get; set; } = null!;
}