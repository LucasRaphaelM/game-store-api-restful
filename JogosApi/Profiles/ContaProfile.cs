using AutoMapper;
using JogosApi.Data.Dtos;
using JogosApi.Models;

namespace JogosApi.Profiles;

public class ContaProfile : Profile
{
    public ContaProfile()
    {
        CreateMap<Conta, ReadContaDto>()
            .ForMember(contaDto => contaDto.Pedidos,
            opts => opts.MapFrom(conta => conta.Pedidos));
    }
}
