using JogosApi.Data.Dtos;
using System.ComponentModel.DataAnnotations;

namespace JogosApi.Models;

public class Conta
{
    [Key]
    [Required]
    public int Id { get; set; }

    [Required]
    public string UsuarioId { get; set; }

    public Usuario Usuario { get; set; }

    public virtual ICollection<Pedido> Pedidos { get; set; }


}
