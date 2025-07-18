using System.Reflection.Metadata;

namespace JogosApi.Data.Dtos;

public class ReadContaDto
{
    public ICollection<ReadPedidoDto> Pedidos { get; set; }
}
