using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JogosApi.Controllers;

[ApiController]
[Route("[controller]")]
public class AccessController : Controller
{
    [HttpGet]
    [Authorize]
    public IActionResult get()
    {
        return Ok("Acesso Permitido!");
    }
}
