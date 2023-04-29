using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[Route("API/[controller]/")]
[ApiController]
[Authorize]
public class AttachController : ControllerBase
{
    private readonly IAttachService _attachService;

    public AttachController(IAttachService attachService)
    {
        _attachService = attachService;
    }

    [HttpPost]
    public async Task<Guid> Create(IFormFile file)
    {
        var attach = new Attach()
        {
            Name = file.Name,
            Size = file.Length,
            MimeType = file.ContentType,
        };

        using var fileStream = file.OpenReadStream();
        return await _attachService.SaveAttach(attach, fileStream);
    }

    [HttpGet]
    public async Task<FileResult> Get(Guid attachId, bool download = false)
    {
        var attach = await _attachService.GetById(attachId);
        var fileStream = _attachService.GetStream(attach.Id);

        return download ? File(fileStream, attach.MimeType, attach.Name) : File(fileStream, attach.MimeType);
    }
}