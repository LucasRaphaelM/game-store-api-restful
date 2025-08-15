using AutoMapper;
using JogosApi.Data.Dtos;
using JogosApi.Models;

namespace JogosApi.Profiles;

public class KeyProfile : Profile
{
    public KeyProfile()
    {
        CreateMap<CreateKeyDto, Key>();
        CreateMap<UpdateKeyDto, Key>();
        CreateMap<Key, ReadKeyDto>()
            .ForMember(key => key.JogoNome,
            opt => opt.MapFrom(key => key.Jogo.Titulo))
            .ForMember(key => key.JogoDesenvolvedora,
            opt => opt.MapFrom(key =>key.Jogo.Desenvolvedora))
            .ForMember(key => key.JogoPic,
            opt => opt.MapFrom(key => key.Jogo.Foto));
    }
}
