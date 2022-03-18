
namespace JetWeb.API.Models
{
    public class Produto
    {
        public int ProdutoId { get; set; }
        public string Nome { get; set; }
        public string Imagem { get; set; }
        public string Descricao { get; set; }
        public int Estoque { get; set; }
        public bool Status { get; set; }
        public decimal Preco { get; set; }
    }
}