namespace DevBrecho.Models
{
    public class PecaCadastrada
    {
        public int PecaCadastradaId { get; set; }
        public string CodigoDaPeca { get; set; }
        public int BolsaId { get; set; }
        public Bolsa? Bolsa { get; set; }
    }
}
