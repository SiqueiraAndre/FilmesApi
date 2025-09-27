using System.ComponentModel.DataAnnotations;

namespace FilmesApi.Data.Dtos;

public class CreateCinemaDto
{
    [Required(ErrorMessage = "O nome do cinema é obrigatório")]
    [MaxLength(100, ErrorMessage = "O nome do cinema não pode exceder 100 caracteres")]
    public string Nome { get; set; }
    public int? EnderecoId { get; set; }
}