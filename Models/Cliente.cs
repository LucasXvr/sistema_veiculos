using System.ComponentModel.DataAnnotations;

public class Cliente
{
    [Key]
    public int Id { get; set; }

    public int ClienteId { get; set; }

    [Required(ErrorMessage = "O campo Nome é obrigatório.")]
    public string Nome { get; set; }

    [Required(ErrorMessage = "O campo Email é obrigatório.")]
    public string Email { get; set; }

    [Required(ErrorMessage = "O campo Data de Nascimento é obrigatório.")]
    public DateTime DataNascimento { get; set; }

    [Required(ErrorMessage = "O campo CPF/CNPJ é obrigatório.")]
    public string CPFCNPJ { get; set; }

    [Required(ErrorMessage = "O campo RG/IE é obrigatório.")]
    public string RGIE { get; set; }

    [Required(ErrorMessage = "O campo Nome de Fantasia é obrigatório.")]
    public string NomeFantasia { get; set; }

    [Required(ErrorMessage = "O campo Endereço é obrigatório.")]
    public string Endereco { get; set; }

    [Required(ErrorMessage = "O campo Número é obrigatório.")]
    public int Numero { get; set; }

    [Required(ErrorMessage = "O campo Complemento é obrigatório.")]
    public string Complemento { get; set; }

    [Required(ErrorMessage = "O campo Bairro é obrigatório.")]
    public string Bairro { get; set; }

    [Required(ErrorMessage = "O campo CEP é obrigatório.")]
    public string CEP { get; set; }

    [Required(ErrorMessage = "O campo WhatsApp é obrigatório.")]
    public string WhatsApp { get; set; }

    [Display(Name = "Fone1")]
    public string? Fone1 { get; set; }
    [Display(Name = "Fone2")]
    public string? Fone2 { get; set; }
    [Display(Name = "Fone3")]
    public string? Fone3 { get; set; }
    public string Classificacao { get; set; }
    public string Observacoes { get; set; }
    public List<Veiculo> Veiculos { get; set; } = new List<Veiculo>();

}