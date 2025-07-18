using JogosApi.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace JogosApi.Data;

public class JogoContext : IdentityDbContext<Usuario>
{
    public JogoContext(DbContextOptions<JogoContext> opts) : base(opts)
    {
        
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        /*modelBuilder.Entity<Key>()
            .HasOne(key => key.Jogo)
            .WithMany(jogo => jogo.Keys)
            .HasForeignKey(key => key.JogoId);*/

        /*--------------------------------------*/


        modelBuilder.Entity<Usuario>()
            .HasOne(usuario => usuario.Conta)
            .WithOne(conta => conta.Usuario)
            .HasForeignKey<Conta>(conta => conta.UsuarioId);

        /*--------------------------------------*/

        modelBuilder.Entity<Pedido>()
            .HasOne(pedido => pedido.Conta)
            .WithMany(conta => conta.Pedidos)
            .HasForeignKey(pedido => pedido.ContaId);

        modelBuilder.Entity<Pedido>()
            .HasOne(pedido => pedido.Key)
            .WithOne(key => key.Pedido)
            .HasForeignKey<Pedido>(pedido => pedido.KeyId);
    }


    public DbSet<Jogo> Jogos { get; set; }
    public DbSet<Key> Keys { get; set; }
    public DbSet<Conta> Contas { get; set; }
    public DbSet<Pedido> Pedidos { get; set; }
}


/*DbContext*/