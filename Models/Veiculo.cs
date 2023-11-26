// Veiculo.cs
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Numerics;

public class Veiculo
{
    [Key]
    public int Id { get; set; }
    public int Grupo { get; set; }
    public string Unidade { get; set; }
    public decimal PrCusto { get; set; }
    public decimal Margem { get; set; }
    public decimal PrVenda { get; set; }
    public string Ncm { get; set; }
    public List<Foto> Fotos { get; set; } = new List<Foto>();
    public string Ativo { get; set; }
    public string CFOP { get; set; }
    public string CEST { get; set; }
    public string PrVendaPrazo { get; set; }
    public DateTime DtCadastro { get; set; }
    public int ViaReciboVeiculo { get; set; }
    public string RenavamVeiculo { get; set; }
    public string RegistroVeiculo { get; set; }
    public string RntrcVeiculo { get; set; }
    public int ExerEmisDocVeiculo { get; set; }
    public string PlacaVeiculo { get; set; }
    public string ChassiVeiculo { get; set; }
    public string CombustivelVeiculo { get; set; }
    public int FabricacaoVeiculo { get; set; }
    public int ModeloVeiculo { get; set; }
    public string CorVeiculo { get; set; }
    public string CategoriaVeiculo { get; set; }
    public DateTime EmissaoDocumentoVeiculo { get; set; }
    public string LocalRegistroVeiculo { get; set; }
    public string CapPotCilVeiculo { get; set; }
    public string MarcaModeloVeiculo { get; set; }
    public string EspecieVeiculo { get; set; }
    public string Observacoes { get; set; }

}

public class Foto
{
    public int Id { get; set; }
    public int VeiculoId { get; set; }
    public string NomeArquivo { get; set; }
    public Veiculo Veiculo { get; set; }
}
