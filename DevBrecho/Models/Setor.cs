namespace DevBrecho.Models
{
    public class Setor
    {
        public int SetorId { get; set; }
        public string Nome { get; set; }
        public ICollection<Bolsa>? Bolsas { get; set; }

    }
}
