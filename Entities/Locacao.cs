namespace Motos.Entities
{
    public class Locacao
    {
        public int Id { get; set; }
        public int EntregadorId { get; set; }
        public Entregador Entregador { get; set; }

        public int MotoId { get; set; }
        public Moto Moto { get; set; }

        public DateTime DataInicio { get; set; }
        public DateTime DataTermino { get; set; }
        public DateTime DataPrevisaoTermino { get; set; }
        public int Plano { get; set; } // Plano de locação (7, 15, 30 dias etc.)
        public decimal ValorTotal { get; set; }
        public decimal Multa { get; set; }
    }
}
