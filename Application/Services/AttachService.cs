using System.Reflection;
using Application.Exceptions;
using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services;

public class AttachService : IAttachService
{
    private readonly string _attachPath;
    private readonly IDatabaseContext _database;

    public AttachService(IDatabaseContext database)
    {
        _database = database;
        _attachPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!, "attaches");
        Directory.CreateDirectory(_attachPath);
    }

    public async Task<Guid> SaveAttach(Attach attach, Stream fileStream)
    {
        attach.Id = Guid.NewGuid();
        var filePath = Path.Combine(_attachPath, attach.Id.ToString());
        using (var stream = File.Create(filePath))
            await fileStream.CopyToAsync(stream);

        await _database.Attaches.AddAsync(attach);
        await _database.SaveChangesAsync();
        return attach.Id;
    }

    public async Task<Attach> GetById(Guid id)
    {
        var attach = await _database.Attaches.FindAsync(id);
        if (attach == default)
            throw new NotFoundException("Attach not found");

        return attach;
    }

    public FileStream GetStream(Guid id)
    {
        var filePath = Path.Combine(_attachPath, id.ToString());

        if (!File.Exists(filePath))
            throw new NotFoundException("File file not found");

        return new FileStream(filePath, FileMode.Open);
    }
}