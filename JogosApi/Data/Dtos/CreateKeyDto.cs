using System.ComponentModel.DataAnnotations;

namespace JogosApi.Data.Dtos;

public class CreateKeyDto
{
    [Required]
    public string KeyCode { get; set; }

    public bool Disponibilidade { get; set; } = true;

    [Required]
    public int JogoId { get; set; }
}
