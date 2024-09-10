namespace Motos.Entities
{
    public class Entregador
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Cnpj { get; set; } // CNPJ único
        public DateTime DataNascimento { get; set; }
        public string NumeroCnh { get; set; } // CNH única
        public string TipoCnh { get; set; } // A, B ou A+B
        public string ImagemCnh { get; set; } // Caminho para o armazenamento da imagem da CNH
    }
}
