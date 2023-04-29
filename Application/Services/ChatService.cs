using Application.DTO.Chat;
using Application.DTO.Common;
using Application.Exceptions;
using Application.Extensions;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Application.Services;

public class ChatService : IChatService
{
    private readonly IMapper _mapper;
    private readonly IDatabaseContext _database;
    private readonly User _currentUser;

    public ChatService(IMapper mapper, IDatabaseContext database, ICurrentUserService currentUserService)
    {
        _mapper = mapper;
        _database = database;
        _currentUser = currentUserService.User;
    }


    public async Task<PaginatedList<ChatDto>> Search(ChatSearchDto searchDto)
    {
        IQueryable<Chat> query = _database.Chats;

        if (string.IsNullOrEmpty(searchDto.Title))
            query = query.Where(x => EF.Functions.ILike(x.Title, $"%{searchDto.Title}%"));

        return await query.ProjectToPaginatedList<Chat, ChatDto>(_mapper.ConfigurationProvider, searchDto);
    }

    public async Task<Guid> Create(ChatCreateDto createDto)
    {
        var chat = _mapper.Map<Chat>(createDto);
        chat.Users = new List<User>()
        {
            _currentUser
        };

        var chatExists = await _database.Chats.AnyAsync(x => x.Title == createDto.Title && x.Users.Contains(_currentUser));
        if (chatExists)
            throw new ConflictException("Chat already exists");

        if (chat.LogoId != default)
        {
            var attachExists = await _database.Attaches.AnyAsync(x => x.Id == chat.LogoId);
            if (!attachExists)
                throw new NotFoundException("Logo not found");
        }

        await _database.Chats.AddAsync(chat);
        await _database.SaveChangesAsync();
        return chat.Id;
    }

    public async Task Update(Guid id, ChatUpdateDto updateDto)
    {
        var chat = await FindChat(id);

        if (updateDto.LogoId != default)
        {
            var attachExists = await _database.Attaches.AnyAsync(x => x.Id == chat.LogoId);
            if (!attachExists)
                throw new NotFoundException("Logo not found");
        }

        chat = _mapper.Map(updateDto, chat);
        _database.Chats.Update(chat);
        await _database.SaveChangesAsync();
    }

    public async Task<PaginatedList<MessageDto>> SearchMessages(Guid chatId, MessageSearchDto searchDto)
    {
        IQueryable<Message> query = _database.Messages.Where(x => x.ChatId == chatId);

        if (string.IsNullOrEmpty(searchDto.Text))
            query = query.Where(x => EF.Functions.ILike(x.Text, $"%{searchDto.Text}%"));

        return await query.ProjectToPaginatedList<Message, MessageDto>(_mapper.ConfigurationProvider, searchDto);
    }

    public async Task<Guid> CreateMessage(Guid chatId, MessageCreateDto createDto)
    {
        var chatExists = await _database.Chats.AnyAsync(x => x.Id == chatId && x.Users.Contains(_currentUser));
        if (!chatExists)
            throw new NotFoundException("Chat not found");

        var message = _mapper.Map<Message>(createDto);
        message.AuthorId = _currentUser.Id;
        message.ChatId = chatId;

        await _database.Messages.AddAsync(message);
        await _database.SaveChangesAsync();
        return message.Id;
    }

    private async Task<Chat> FindChat(Guid id)
    {
        var chat = await _database.Chats.FindAsync(id);
        if (chat == default)
            throw new NotFoundException("Chat not found");

        return chat;
    }
}