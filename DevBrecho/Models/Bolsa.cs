namespace DevBrecho.Models
{
    public class Bolsa
    {
        public int BolsaId { get; set; }
        public DateTime DataDeEntrada { get; set; }
        public DateTime DataMensagem { get; set; }
        public int FornecedoraId { get; set; }
        public Fornecedora? Fornecedora { get; set; }
        public int SetorId { get; set; }
        public Setor? Setor { get; set; }
        public ICollection<PecaCadastrada>? CodigosDePecaCadastrada { get; set; }
        public int QuantidadeDePecasSemCadastro { get; set; }
        public string? Observacoes { get; set; }
    }
}
