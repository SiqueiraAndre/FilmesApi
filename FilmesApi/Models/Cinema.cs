using System.ComponentModel.DataAnnotations;

public class Cinema
{
    [Key]
    [Required]
    public int Id { get; set; }
    [Required(ErrorMessage = "O nome do cinema é obrigatório")]
    [MaxLength(100, ErrorMessage = "O nome do cinema não pode exceder 100 caracteres")]
    public string Nome { get; set; }
    
}