using AutoMapper;
using JogosApi.Data;
using JogosApi.Data.Dtos;
using JogosApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace JogosApi.Controllers;

[ApiController]
[Route("[controller]")]
public class PedidoController : ControllerBase
{
    private JogoContext _context;
    private IMapper _mapper;

    public PedidoController(IMapper mapper, JogoContext jogoContext)
    {
        _mapper = mapper;
        _context = jogoContext;
    }

    [HttpPost]
    public IActionResult CriarPedido([FromBody] CreatePedidoDto pedidoDto)
    {
        Key key = _context.Keys.FirstOrDefault(key => key.Id == pedidoDto.KeyId);
        Conta conta = _context.Contas.FirstOrDefault(conta => conta.Id == pedidoDto.ContaId);

        if (key == null || !key.Disponibilidade || conta == null)
        {
            return BadRequest("Key ou conta inválida, ou já foi vendida.");
        }

        Pedido pedido = _mapper.Map<Pedido>(pedidoDto);
        pedido.DataCompra = DateTime.Now;

        key.Disponibilidade = false;

        _context.Add(pedido);
        _context.SaveChanges();

        return Ok(pedidoDto);
    }
}
