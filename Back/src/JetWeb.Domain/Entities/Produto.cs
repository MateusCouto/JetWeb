
namespace JetWeb.Domain.Entities
{
    public class Produto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Imagem { get; set; }
        public string Descricao { get; set; }
        public int Estoque { get; set; }
        public bool Status { get; set; } = true;
        public decimal Preco { get; set; }
    }
}