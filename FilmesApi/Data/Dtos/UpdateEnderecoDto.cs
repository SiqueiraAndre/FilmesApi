using System.ComponentModel.DataAnnotations;

namespace FilmesApi.Data.Dtos;

public class UpdateEnderecoDto
{
    public string Logradouro { get; set; }
    [Required(ErrorMessage = "O campo bairro é obrigatório")]
    public string Bairro { get; set; }
    [Required(ErrorMessage = "O campo número é obrigatório")]
    public string Numero { get; set; }
    public string Complemento { get; set; }
    [Required(ErrorMessage = "O campo cidade é obrigatório")]
    public string Cidade { get; set; }
}