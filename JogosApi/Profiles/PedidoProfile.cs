using AutoMapper;
using JogosApi.Data.Dtos;
using JogosApi.Models;

namespace JogosApi.Profiles;

public class PedidoProfile : Profile
{
    public PedidoProfile()
    {
        CreateMap<CreatePedidoDto, Pedido>();
        CreateMap<Pedido, ReadPedidoDto>();
    }
}
