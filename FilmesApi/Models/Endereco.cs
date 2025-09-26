using System.ComponentModel.DataAnnotations;

namespace FilmesApi.Models;

public class Endereco
{
    [Key]
    [Required]
    public int Id { get; set; }
    [Required(ErrorMessage = "O campo logradouro é obrigatório")]
    public string Logradouro { get; set; }
    [Required(ErrorMessage = "O campo bairro é obrigatório")]
    public string Bairro { get; set; }
    [Required(ErrorMessage = "O campo número é obrigatório")]
    public string Numero { get; set; }
    public string Complemento { get; set; }
    [Required(ErrorMessage = "O campo cidade é obrigatório")]
    public string Cidade { get; set; }
    public virtual Cinema Cinema { get; set; }
}