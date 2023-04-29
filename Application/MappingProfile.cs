using Application.DTO.Chat;
using Application.DTO.Profile;
using Application.DTO.User;
using Domain.Entities;
using Isopoh.Cryptography.Argon2;
using Profile = AutoMapper.Profile;

namespace Application;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<UserCreateDto, User>()
            .ForMember(d => d.PasswordHash, m => m.MapFrom(s =>
                Argon2.Hash(s.Password, 3, 65536, Environment.ProcessorCount, Argon2Type.HybridAddressing, 32, null)));
        CreateMap<UserUpdateDto, User>();

        CreateMap<User, UserBriefDto>();
        CreateMap<ProfileCreateDto, Domain.Entities.Profile>();
        CreateMap<ProfileUpdateDto, Domain.Entities.Profile>();

        CreateMap<Settings, SettingsDto>().ReverseMap();

        CreateMap<ChatCreateDto, Chat>();
        CreateMap<ChatUpdateDto, Chat>();

        CreateMap<MessageCreateDto, Message>();


        // projections
        CreateProjection<User, UserBriefDto>();
        CreateProjection<User, UserDto>();
        CreateProjection<Domain.Entities.Profile, ProfileDto>()
            .ForMember(d => d.AttacheIds, m => m.MapFrom(s => s.Attaches.Select(x => s.Id)));


        CreateProjection<Chat, ChatDto>();

        CreateProjection<Message, MessageDto>();
    }
}