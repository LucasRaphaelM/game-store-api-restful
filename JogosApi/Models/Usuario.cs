using AutoMapper;
using Microsoft.AspNetCore.Identity;


namespace JogosApi.Models;

public class Usuario : IdentityUser
{
    public Usuario() : base() { }

    public bool UserAdmin { get; set; } = false;

    public virtual Conta Conta { get; set; }
}
