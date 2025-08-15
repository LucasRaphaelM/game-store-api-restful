using System.ComponentModel.DataAnnotations;

namespace JogosApi.Data.Dtos;

public class CreatePedidoDto
{
    [Required]
    public int ContaId { get; set; }

    [Required]
    public int JogoId { get; set; }
}
