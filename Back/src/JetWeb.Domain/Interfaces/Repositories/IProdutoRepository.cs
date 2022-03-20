using System.Threading.Tasks;
using JetWeb.Domain.Entities;

namespace JetWeb.Domain.Interfaces.Repositories
{
    public interface IProdutoRepository
    {
        Task<Produto[]> GetAllProdutosByNome(string nome);
        Task<Produto[]> GetAllProdutos();
        Task<Produto> GetProdutoById(int produtoId);
    }
}