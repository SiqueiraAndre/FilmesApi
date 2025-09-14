using System.ComponentModel.DataAnnotations;

namespace FilmesApi.Data.Dtos;

public class UsuarioDto
{
    [Required]
    public string Username { get; set; }
    [Required]
    public string Password { get; set; }
}