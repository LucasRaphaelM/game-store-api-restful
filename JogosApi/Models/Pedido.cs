using System.ComponentModel.DataAnnotations;

namespace JogosApi.Models;

public class Pedido
{
    [Key]
    [Required]
    public int Id { get; set; }

    [Required]
    public int ContaId { get; set; }
    public Conta Conta { get; set; }

    [Required]
    public int KeyId { get; set; }
    public Key Key { get; set; }

    [DataType(DataType.DateTime)]
    public DateTime DataCompra { get; set; }
}
