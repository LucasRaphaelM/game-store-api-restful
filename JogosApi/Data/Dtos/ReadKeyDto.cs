using JogosApi.Models;
using System.Globalization;

namespace JogosApi.Data.Dtos;

public class ReadKeyDto
{
    public string KeyCode { get; set; }

    public bool Disponibilidade { get; set; } = true;

    public int JogoId { get; set; }

    public string JogoNome { get; set; }

    public string JogoPic { get; set; }
}
