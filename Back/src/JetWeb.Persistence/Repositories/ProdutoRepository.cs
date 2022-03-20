using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetWeb.Domain.Entities;
using JetWeb.Domain.Interfaces.Repositories;
using JetWeb.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace JetWeb.Persistence.Repositories
{
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly JetWebContext _context;
        public ProdutoRepository(JetWebContext context)
        {
            _context = context;
        }

        public async Task<Produto[]> GetAllProdutos()
        {
            IQueryable<Produto> query = _context.Produtos;

            query = query.AsNoTracking().OrderBy(p => p.Id);

            return await query.ToArrayAsync();
        }

        public async Task<Produto[]> GetAllProdutosByNome(string nome)
        {
            IQueryable<Produto> query = _context.Produtos;

            query = query.AsNoTracking().OrderBy(p => p.Id)
                         .Where(p => p.Nome.ToLower().Contains(nome.ToLower()));

            return await query.ToArrayAsync();
        }

        public async Task<Produto> GetProdutoById(int produtoId)
        {
            IQueryable<Produto> query = _context.Produtos;

            query = query.AsNoTracking().OrderBy(p => p.Id)
                         .Where(p => p.Id == produtoId);

            return await query.FirstOrDefaultAsync();
        }
    }
}