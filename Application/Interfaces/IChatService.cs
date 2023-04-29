using Application.DTO.Chat;
using Application.DTO.Common;

namespace Application.Interfaces;

public interface IChatService
{
    Task<PaginatedList<ChatDto>> Search(ChatSearchDto searchDto);
    Task<Guid> Create(ChatCreateDto createDto);
    Task Update(Guid id, ChatUpdateDto updateDto);
    Task<PaginatedList<MessageDto>> SearchMessages(Guid chatId, MessageSearchDto searchDto);
    Task<Guid> CreateMessage(Guid chatId, MessageCreateDto createDto);
}