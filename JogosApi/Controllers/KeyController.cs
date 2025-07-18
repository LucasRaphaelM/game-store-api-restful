using AutoMapper;
using JogosApi.Data;
using JogosApi.Data.Dtos;
using JogosApi.Models;
using JogosApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;

namespace JogosApi.Controllers;

[ApiController]
[Route("[controller]")]
public class KeyController : ControllerBase
{
    private JogoContext _context;
    private IMapper _mapper;

    public KeyController(JogoContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpPost("adiciona-key")]
    public IActionResult AdicionaKey([FromBody] CreateKeyDto keyDto, [FromServices] KeyEncryptionService crypto)
    {
        var keyCriptografada = crypto.Criptografar(keyDto.KeyCode);
        Key key = _mapper.Map<Key>(keyDto);
        key.KeyCode = keyCriptografada;
        //Jogo jogo = new Jogo();
        _context.Keys.Add(key);
        _context.SaveChanges();
        return CreatedAtAction(nameof(RecuperaKeys), new { Id =  key.Id },keyDto);
    }

    [HttpGet]
    public IEnumerable<ReadKeyDto> RecuperaKeys()
    {
        return _mapper.Map<List<ReadKeyDto>>(_context.Keys.Include(key => key.Jogo).ToList());
    }

    [HttpGet("{id}")]
    public IActionResult? RecuperaKeys(int id)
    {
        Key key = _context.Keys.Include(key => key.Jogo).FirstOrDefault(key => key.Id == id);
        if (key == null) return NotFound();
        ReadKeyDto keyDto = _mapper.Map<ReadKeyDto>(key);
        return Ok(keyDto);
    }

    [HttpPut("{id}")]
    public IActionResult? AtualizaKeys(int id, [FromBody] UpdateKeyDto keyDto)
    {
        Key key = _context.Keys.FirstOrDefault(key => key.Id == id);
        if (key == null) return NotFound();
        
        _mapper.Map(keyDto, key);
        _context.SaveChanges();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult? DeletaKeys(int id)
    {
        Key key = _context.Keys.FirstOrDefault(key => key.Id == id);
        if (key == null) return NotFound();

        _context.Remove(key);
        _context.SaveChanges();
        return NoContent();
    }
}
