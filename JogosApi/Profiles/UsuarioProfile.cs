using AutoMapper;
using JogosApi.Data.Dtos;
using JogosApi.Models;
using Microsoft.EntityFrameworkCore.Metadata;

namespace JogosApi.Profiles;

public class UsuarioProfile : Profile
{
    public UsuarioProfile()
    {
        CreateMap<CreateUsuarioDto, Usuario>();
    }
}
