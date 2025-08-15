using AutoMapper;
using JogosApi.Data;
using JogosApi.Data.Dtos;
using JogosApi.Models;
using JogosApi.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JogosApi.Controllers;

[ApiController]
[Route("[controller]")]
public class UsuarioController : ControllerBase
{
    private JogoContext _context;
    private IMapper _mapper;
    private UserManager<Usuario> _userManager;
    private SignInManager<Usuario> _signInManager;

    public UsuarioController(IMapper mapper, UserManager<Usuario> userManager, SignInManager<Usuario> signInManager, JogoContext context)
    {
        _mapper = mapper;
        _userManager = userManager;
        _signInManager = signInManager;
        _context = context;
    }

    [HttpPost("cadastro")]
    public async Task<IActionResult> CadastraUsuario(CreateUsuarioDto usuarioDto)
    {
        Usuario usuario = _mapper.Map<Usuario>(usuarioDto);

        IdentityResult resultado = await _userManager.CreateAsync(usuario, usuarioDto.Password);

        if (!resultado.Succeeded)
            throw new ApplicationException("Falha ao cadastrar usuário");

        Conta conta = new Conta
        {
            UsuarioId = usuario.Id,
            Pedidos = new List<Pedido>()
        };

        _context.Contas.Add(conta);
        await _context.SaveChangesAsync();

        return Ok("Usuário cadastrado");

    }

    [HttpPost("login")]
    public async Task<IActionResult> LoginUsuario([FromBody] LoginUsuarioDto usuarioDto)
    {
        var resultado = await _signInManager.PasswordSignInAsync(usuarioDto.Username, usuarioDto.Password, false, false);

        if (!resultado.Succeeded)
        {
            return Unauthorized("Usuário ou senha inválidos");
        }

        var usuario = await _signInManager.UserManager.FindByNameAsync(usuarioDto.Username);

        var claims = new[]
        {
        new Claim("username", usuario.UserName),
        new Claim("id", usuario.Id),
        new Claim("admin", usuario.UserAdmin.ToString())
    };

        var chave = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("SAOhsd0ASDhaASDs0GAGhshHA7sVAh08AS"));
        var signingCredentials = new SigningCredentials(chave, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: "http://127.0.0.1:5500",
            audience: "http://127.0.0.1:5500",
            claims: claims,
            expires: DateTime.Now.AddDays(1),
            signingCredentials: signingCredentials
        );

        var tokenGerado = new JwtSecurityTokenHandler().WriteToken(token);

        return Ok(new { token = tokenGerado });
    }

    [HttpGet("{id}")]
    public IActionResult RecuperaUsuario(string id, [FromServices] KeyEncryptionService crypto)
    {
        var usuario = _context.Users
            .Include(usuario => usuario.Conta)
                .ThenInclude(conta => conta.Pedidos)
                    .ThenInclude(pedidos => pedidos.Key)
                        .ThenInclude(key => key.Jogo)
            .FirstOrDefault(usuario => usuario.Id == id);

        if (usuario == null || usuario.Conta == null)
            return NotFound("Usuário ou conta não encontrados");

        var usuarioDto = new ReadUsuarioDto
        {
            Nome = usuario.UserName,
            Conta = new ReadContaDto
            {
                Pedidos = usuario.Conta.Pedidos
                .Select(pedido => new ReadPedidoDto
                {
                    DataCompra = pedido.DataCompra,
                    Key = new ReadKeyDto
                    {
                        KeyCode = crypto.Descriptografar(pedido.Key.KeyCode),
                        JogoNome = pedido.Key.Jogo.Titulo,
                        JogoId = pedido.Key.Jogo.Id,
                        JogoPic = pedido.Key.Jogo.Foto,
                        JogoDesenvolvedora = pedido.Key.Jogo.Desenvolvedora,
                    }
                }).ToList()
            }
        };

        return Ok(usuarioDto);
    }
}
