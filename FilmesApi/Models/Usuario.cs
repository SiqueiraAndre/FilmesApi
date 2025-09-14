using System.ComponentModel.DataAnnotations;

namespace FilmesApi.Models;

public class Usuario
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string Username { get; set; }
    [Required]
    public string Password { get; set; }
}