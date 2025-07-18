using System.ComponentModel.DataAnnotations;

namespace JogosApi.Models;

public class Jogo
{
    [Key]
    [Required]
    public int Id { get; set; }

    [Required(ErrorMessage = "Título do jogo é obrigatório")]
    public string Titulo { get; set; }

    [Required(ErrorMessage = "Foto do jogo é obrigatório")]
    public string Foto { get; set; }

    [Required(ErrorMessage = "Desenvolvedora do jogo é obrigatório")]
    public string Desenvolvedora { get; set; }

    [Required(ErrorMessage = "Valor do jogo é obrigatório")]
    [Range(0, 10000, ErrorMessage = "Valor mínimo 0 e valor máximo 10000")]
    public float Preco { get; set; }

    public virtual ICollection<Key> Keys { get; set; }
}
