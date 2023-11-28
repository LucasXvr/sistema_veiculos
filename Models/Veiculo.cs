// Veiculo.cs
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Numerics;

public class Veiculo
{
    [Key]
    public int Id { get; set; }

    [Required(ErrorMessage = "O campo Grupo é obrigatório.")]
    public int Grupo { get; set; }

    [Required(ErrorMessage = "O campo Unidade é obrigatório.")]
    public string Unidade { get; set; }

    [Required(ErrorMessage = "O campo Preço de Custo é obrigatório.")]
    public decimal PrCusto { get; set; }

    [Required(ErrorMessage = "O campo Margem é obrigatório.")]
    public decimal Margem { get; set; }

    [Required(ErrorMessage = "O campo Preço de Venda é obrigatório.")]
    public decimal PrVenda { get; set; }

    [Required(ErrorMessage = "O campo NCM é obrigatório.")]
    public string Ncm { get; set; }

    public List<Foto> Fotos { get; set; } = new List<Foto>();

    [Required(ErrorMessage = "O campo Ativo é obrigatório.")]
    public string Ativo { get; set; }

    [Required(ErrorMessage = "O campo CFOP é obrigatório.")]
    public string CFOP { get; set; }

    [Required(ErrorMessage = "O campo CEST é obrigatório.")]
    public string CEST { get; set; }

    [Required(ErrorMessage = "O campo Preço de Venda à Prazo é obrigatório.")]
    public string PrVendaPrazo { get; set; }

    [Required(ErrorMessage = "O campo Data de Cadastro é obrigatório.")]
    public DateTime DtCadastro { get; set; }

    [Required(ErrorMessage = "O campo Via do Recibo do Veículo  é obrigatório.")]
    public int ViaReciboVeiculo { get; set; }

    [Required(ErrorMessage = "O campo RENAVAM é obrigatório.")]
    public string RenavamVeiculo { get; set; }

    [Required(ErrorMessage = "O campo Registro do Veículo é obrigatório.")]
    public string RegistroVeiculo { get; set; }

    [Required(ErrorMessage = "O campo RNTRC do Veículo é obrigatório.")]
    public string RntrcVeiculo { get; set; }

    [Required(ErrorMessage = "O campo Exercício de Emissão de Documento de Veículo é obrigatório.")]
    public int ExerEmisDocVeiculo { get; set; }

    [Required(ErrorMessage = "O campo Placa do Veículo é obrigatório.")]
    public string PlacaVeiculo { get; set; }

    [Required(ErrorMessage = "O campo Chassi do Veículo é obrigatório.")]
    public string ChassiVeiculo { get; set; }

    [Required(ErrorMessage = "O campo Combustível do Veículo é obrigatório.")]
    public string CombustivelVeiculo { get; set; }

    [Required(ErrorMessage = "O campo Fabricação do Veículo é obrigatório.")]
    public int FabricacaoVeiculo { get; set; }

    [Required(ErrorMessage = "O campo Modelo do Veículo é obrigatório.")]
    public int ModeloVeiculo { get; set; }

    [Required(ErrorMessage = "O campo Cor do Veículo é obrigatório.")]
    public string CorVeiculo { get; set; }

    [Required(ErrorMessage = "O campo Categoria do Veículo é obrigatório.")]
    public string CategoriaVeiculo { get; set; }

    [Required(ErrorMessage = "O campo Emissão do Documento do Veículo é obrigatório.")]
    public DateTime EmissaoDocumentoVeiculo { get; set; }

    [Required(ErrorMessage = "O campo Local do Registro do Veículo é obrigatório.")]
    public string LocalRegistroVeiculo { get; set; }

    [Required(ErrorMessage = "O campo Capacidade, Potencia e Cilindrada do Veículo é obrigatório.")]
    public string CapPotCilVeiculo { get; set; }

    [Required(ErrorMessage = "O campo Marca Modelo do Veículo é obrigatório.")]
    public string MarcaModeloVeiculo { get; set; }

    [Required(ErrorMessage = "O campo Espécie do Veículo é obrigatório.")]
    public string EspecieVeiculo { get; set; }

    public string Observacoes { get; set; }
    public int ClienteId { get; set; }
    public Cliente Cliente { get; set; }
}

public class Foto
{
    public int Id { get; set; }
    public int VeiculoId { get; set; }
    public string NomeArquivo { get; set; }
    public Veiculo Veiculo { get; set; }
}
