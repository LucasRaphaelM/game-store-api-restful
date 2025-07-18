using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System.ComponentModel.DataAnnotations;

namespace JogosApi.Models;

public class Key
{
    [Key]
    [Required]
    public int Id { get; set; }

    public string? KeyCode { get; set; }

    public bool Disponibilidade { get; set; }

    public int JogoId { get; set; }
    public virtual Jogo Jogo { get; set; }

    public virtual Pedido? Pedido { get; set; }
}
