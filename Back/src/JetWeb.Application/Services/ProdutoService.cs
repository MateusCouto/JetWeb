using System;
using System.Threading.Tasks;
using AutoMapper;
using JetWeb.Application.Dtos;
using JetWeb.Application.Interfaces.Services;
using JetWeb.Domain.Entities;
using JetWeb.Domain.Interfaces.Repositories;

namespace JetWeb.Application.Services
{
    public class ProdutoService : IProdutoService
    {
        private readonly IBaseRepository _baseRepository;
        private readonly IProdutoRepository _produtoRepository;
        private readonly IMapper _mapper;
        public ProdutoService(IBaseRepository baseRepository,
                             IProdutoRepository produtoRepository,
                             IMapper mapper)
        {
            _baseRepository = baseRepository;
            _produtoRepository = produtoRepository;
            _mapper = mapper;
        }
        public async Task<ProdutoDto> AddProdutos(ProdutoDto model)
        {
            try
            {
                var produto = _mapper.Map<Produto>(model);

                _baseRepository.Add<Produto>(produto);

                if (await _baseRepository.SaveChangesAsync())
                {
                    var produtoRetorno = await _produtoRepository.GetProdutoById(produto.Id);

                    return _mapper.Map<ProdutoDto>(produtoRetorno);
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ProdutoDto> UpdateProduto(int produtoId, ProdutoDto model)
        {
            try
            {
                var produto = await _produtoRepository.GetProdutoById(produtoId);
                if (produto == null) return null;

                model.Id = produto.Id;

                _mapper.Map(model, produto);

                _baseRepository.Update<Produto>(produto);

                //teste
                if (await _baseRepository.SaveChangesAsync())
                {
                    var produtoRetorno = await _produtoRepository.GetProdutoById(produto.Id);

                    return _mapper.Map<ProdutoDto>(produtoRetorno);
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> DeleteProduto(int produtoId)
        {
            try
            {
                var produto = await _produtoRepository.GetProdutoById(produtoId);
                if (produto == null) throw new Exception("O Produto para deletar não foi encontrado");

                _baseRepository.Delete<Produto>(produto);
                return await _baseRepository.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ProdutoDto[]> GetAllProdutos()
        {
            try
            {
                var produtos = await _produtoRepository.GetAllProdutos();
                if (produtos == null) return null;

                var resultado = _mapper.Map<ProdutoDto[]>(produtos);

                return resultado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ProdutoDto[]> GetAllProdutosByNome(string nome)
        {
            try
            {
                var produtos = await _produtoRepository.GetAllProdutosByNome(nome);
                if (produtos == null) return null;

                var resultado = _mapper.Map<ProdutoDto[]>(produtos);

                return resultado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ProdutoDto> GetProdutoById(int produtoId)
        {
            try
            {
                var produto = await _produtoRepository.GetProdutoById(produtoId);
                if (produto == null) return null;

                var resultado = _mapper.Map<ProdutoDto>(produto);

                return resultado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> UpdateStatusProduto(int produtoId, bool status)
        {
            try
            {
                var produto = await _produtoRepository.GetProdutoById(produtoId);
                if (produto == null) throw new ArgumentException("Produto não encontrado");

                produto.Status = status;

                _baseRepository.Update<Produto>(produto);

                if (await _baseRepository.SaveChangesAsync()) return true;

                return false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}