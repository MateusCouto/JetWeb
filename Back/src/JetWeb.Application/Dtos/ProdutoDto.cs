
using System.ComponentModel.DataAnnotations;

namespace JetWeb.Application.Dtos
{
    public class ProdutoDto
    {
        public int Id { get; set; }

        //[Required(ErrorMessage = "O campo {0} é obrigtório."),
        [StringLength(150, ErrorMessage = "É permitido no máximo 150 caracteres.")]
        [Display(Name = "Nome do Produto")]
        public string Nome { get; set; }

        [RegularExpression(@".*\.(jpeg|png)$", ErrorMessage = "Não é uma imagem válida. (jpeg e png)")]
        public string Imagem { get; set; }

        //[Required(ErrorMessage = "O campo {0} é obrigtório."),
        [StringLength(2000, ErrorMessage = "É permitido no máximo 2.000 caracteres.")]
        [Display(Name = "Descrição do Produto")]
        public string Descricao { get; set; }
        public int Estoque { get; set; }
        public bool Status { get; set; } = true;

        [Required(ErrorMessage = "O campo {0} é obrigtório.")]
        [Display(Name = "Preço")]
        public decimal Preco { get; set; }
    }
}