using AutoMapper;
using JogosApi.Data.Dtos;
using JogosApi.Models;

namespace JogosApi.Profiles;

public class JogoProfile : Profile
{
    public JogoProfile()
    {
        CreateMap<CreateJogoDto, Jogo>();
        CreateMap<UpdateJogoDto, Jogo>();
        CreateMap<Jogo, UpdateJogoDto>();
        CreateMap<Jogo, ReadJogoDto>()
            .ForMember(jogoDto => jogoDto.Keys,
            opt => opt.MapFrom(jogo => jogo.Keys));
    }
}
