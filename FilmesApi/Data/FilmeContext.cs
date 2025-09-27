using FilmesApi.Models;
using Microsoft.EntityFrameworkCore;

namespace FilmesApi.Data;

public class FilmeContext : DbContext
{
    public FilmeContext(DbContextOptions<FilmeContext> options) 
        : base(options) 
    {
    }

    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<Filme> Filmes { get; set; }
    public DbSet<Cinema> Cinemas { get; set; }
    public DbSet<Endereco> Enderecos { get; set; }
    public DbSet<Sessao> Sessoes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configure one-to-one optional relationship between Cinema and Endereco
        modelBuilder.Entity<Cinema>()
            .HasOne(c => c.Endereco)
            .WithOne(e => e.Cinema)
            .HasForeignKey<Cinema>(c => c.EnderecoId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.SetNull);

        base.OnModelCreating(modelBuilder);
    }

}
