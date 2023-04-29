using Application.DTO.Chat;
using Application.DTO.Common;
using Application.DTO.User;
using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[Route("API/[controller]/")]
[ApiController]
[Authorize]
public class ChatController : ControllerBase
{
    private readonly IChatService _chatService;

    public ChatController(IChatService chatService)
    {
        _chatService = chatService;
    }

    [HttpGet("search")]
    public async Task<PaginatedList<ChatDto>> Search([FromQuery] ChatSearchDto searchDto)
    {
        return await _chatService.Search(searchDto);
    }

    [HttpPost]
    public async Task<Guid> Create(ChatCreateDto createDto)
    {
        return await _chatService.Create(createDto);
    }

    [HttpPut("{id:guid}")]
    public async Task Update(Guid id, ChatUpdateDto updateDto)
    {
        await _chatService.Update(id, updateDto);
    }

    [HttpGet("{chatId:guid}/Message")]
    public async Task<PaginatedList<MessageDto>> SearchMessages(Guid chatId, [FromQuery] MessageSearchDto searchDto)
    {
        return await _chatService.SearchMessages(chatId, searchDto);
    }

    [HttpPost("{chatId:guid}/Message")]
    public async Task<Guid> CreateMessage(Guid chatId, [FromQuery] MessageCreateDto createDto)
    {
        return await _chatService.CreateMessage(chatId, createDto);
    }
}