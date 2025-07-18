using AutoMapper;
using JogosApi.Data;
using JogosApi.Data.Dtos;
using JogosApi.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JogosApi.Controllers;

[ApiController]
[Route("[controller]")]
public class JogoController : ControllerBase
{
    private JogoContext _context;
    private IMapper _mapper;

    public JogoController(JogoContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpPost]
    public IActionResult AdicionaJogo([FromBody] CreateJogoDto jogoDto)
    {
        Jogo jogo = _mapper.Map<Jogo>(jogoDto);
        _context.Jogos.Add(jogo);
        _context.SaveChanges();
        return CreatedAtAction(nameof(RecuperaJogoPorId), new {id = jogo.Id}, jogo);
    }

    [HttpGet]
    public IEnumerable<ReadJogoDto> RecuperaJogos([FromQuery] int skip = 0, [FromQuery] int take = 10)
    {
        return  _mapper.Map<List<ReadJogoDto>>(_context.Jogos.Include(jogo => jogo.Keys).Skip(skip).Take(take).ToList());
    }

    [HttpGet("{id}")]
    public IActionResult? RecuperaJogoPorId(int id)
    {
        var jogo = _context.Jogos.Include(jogo => jogo.Keys).FirstOrDefault(jogo => jogo.Id == id);
        if (jogo == null) return NotFound();
        var jogoDto = _mapper.Map<ReadJogoDto>(jogo);
        return Ok(jogoDto);
    }

    [HttpPut("{id}")]
    public IActionResult AtualizaJogo(int id, [FromBody] UpdateJogoDto jogoDto)
    {
        var jogo = _context.Jogos.FirstOrDefault(jogo => jogo.Id == id);
        if (jogo == null) return NotFound();
        _mapper.Map(jogoDto, jogo);
        _context.SaveChanges();
        return NoContent();
    }

    [HttpPatch("{id}")]
    public IActionResult AtualizaJogoParcial(int id, JsonPatchDocument<UpdateJogoDto> patch)
    {
        var jogo = _context.Jogos.FirstOrDefault(jogo => jogo.Id == id);
        if (jogo == null) return NotFound();

        var jogoParaAtualizar = _mapper.Map<UpdateJogoDto>(jogo);

        patch.ApplyTo(jogoParaAtualizar, ModelState);

        if(!TryValidateModel(jogoParaAtualizar))
        {
            return ValidationProblem(ModelState);
        }
        _mapper.Map(jogoParaAtualizar, jogo);
        _context.SaveChanges();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult DeletaJogo(int id)
    {
        var jogo = _context.Jogos.FirstOrDefault(jogo => jogo.Id == id);
        if (jogo == null) return NotFound();
        _context.Remove(jogo);
        _context.SaveChanges();
        return NoContent();
    }
}
