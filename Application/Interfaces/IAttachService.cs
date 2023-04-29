using Domain.Entities;

namespace Application.Interfaces;

public interface IAttachService
{
    Task<Guid> SaveAttach(Attach attach, Stream fileStream);
    Task<Attach> GetById(Guid id);
    FileStream GetStream(Guid id);
}