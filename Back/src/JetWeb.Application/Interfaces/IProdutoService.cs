using System.Threading.Tasks;
using JetWeb.Application.Dtos;

namespace JetWeb.Application.Interfaces.Services
{
    public interface IProdutoService
    {
        Task<ProdutoDto> AddProdutos(ProdutoDto model);
        Task<ProdutoDto> UpdateProduto(int produtoId, ProdutoDto model);
        Task<bool> DeleteProduto(int produtoId);

        Task<ProdutoDto[]> GetAllProdutos();
        Task<ProdutoDto[]> GetAllProdutosByNome(string nome);
        Task<ProdutoDto> GetProdutoById(int produtoId);
        Task<bool> UpdateStatusProduto(int produtoId, bool status);
    }
}