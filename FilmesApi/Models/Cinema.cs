using System.ComponentModel.DataAnnotations;
using FilmesApi.Models;

public class Cinema
{
    [Key]
    [Required]
    public int Id { get; set; }
    [Required(ErrorMessage = "O nome do cinema é obrigatório")]
    [MaxLength(100, ErrorMessage = "O nome do cinema não pode exceder 100 caracteres")]
    public string Nome { get; set; }
    public int? EnderecoId { get; set; }
    // Make navigation nullable to indicate an optional relationship
    public virtual Endereco? Endereco { get; set; }
    public virtual ICollection<Sessao> Sessoes { get; set; }

}