using JogosApi.Models;
using System.ComponentModel.DataAnnotations;

namespace JogosApi.Data.Dtos;

public class ReadJogoDto
{
    public int Id { get; set; }
    public string Titulo { get; set; }

    public string Foto { get; set; }

    public string Desenvolvedora { get; set; }

    public float Preco { get; set; }

    public ICollection<ReadKeyDto> Keys { get; set; }
}
