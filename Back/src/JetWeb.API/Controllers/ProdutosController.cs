using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using JetWeb.API.Helpers;
using JetWeb.Application.Dtos;
using JetWeb.Application.Interfaces.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace JetWeb.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProdutosController : ControllerBase
    {
        private readonly IProdutoService _produtoService;
        private readonly IUtil _util;
        private readonly string _destino = "Images";
        private readonly IWebHostEnvironment _hostEnvironment;

        public ProdutosController(IProdutoService produtoService, IWebHostEnvironment hostEnvironment, IUtil util)
        {
            _hostEnvironment = hostEnvironment;
            _produtoService = produtoService;
            _util = util;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var produtos = await _produtoService.GetAllProdutos();
                if (produtos == null) return NoContent();

                return Ok(produtos);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar recuperar produtos. Erro: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var produto = await _produtoService.GetProdutoById(id);
                if (produto == null) return NoContent();

                return Ok(produto);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar recuperar produtos. Erro: {ex.Message}");
            }
        }

        [HttpGet("{nome}/nome")]
        public async Task<IActionResult> GetByNome(string nome)
        {
            try
            {
                var produto = await _produtoService.GetAllProdutosByNome(nome);
                if (produto == null) return NoContent();

                return Ok(produto);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar recuperar produtos. Erro: {ex.Message}");
            }
        }

        [HttpPost("upload-image/{produtoId}")]
        public async Task<IActionResult> UploadImage(int produtoId)
        {
            try
            {
                var produto = await _produtoService.GetProdutoById(produtoId);
                if (produto == null) return NoContent();

                var file = Request.Form.Files[0];

                var supportedTypes = new[] { "jpg", "png" };
                var fileExt = System.IO.Path.GetExtension(file.FileName).Substring(1);
                if (!supportedTypes.Contains(fileExt))
                {
                    return StatusCode(StatusCodes.Status415UnsupportedMediaType, "Extensões permitidas: '*.jpg' e '*.png'");
                }

                if (file.Length > 0 && file.Length <= 2097152)
                {
                    _util.DeleteImage(produto.Imagem, _destino);
                    produto.Imagem = await _util.SaveImage(file, _destino);
                }
                else
                {
                    return StatusCode(StatusCodes.Status411LengthRequired, "Tamanho máximo: 2 mb.");
                }

                var ProdutoRetorno = await _produtoService.UpdateProduto(produtoId, produto);

                return Ok(ProdutoRetorno);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar realizar upload da imagem do produto. Erro: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(ProdutoDto model)
        {
            try
            {
                var produto = await _produtoService.AddProdutos(model);
                if (produto == null) return NoContent();

                return Ok(produto);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar adicionar produtos. Erro: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, ProdutoDto model)
        {
            try
            {
                var produto = await _produtoService.UpdateProduto(id, model);
                if (produto == null) return NoContent();

                return Ok(produto);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar atualizar produtos. Erro: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var produto = await _produtoService.GetProdutoById(id);
                if (produto == null) return NoContent();

                if (await _produtoService.DeleteProduto(id))
                {
                    _util.DeleteImage(produto.Imagem, _destino);
                    return Ok(new { message = "Produto Deletado com sucesso" });
                }
                else
                {
                    throw new Exception("Ocorreu um problem não específico ao tentar deletar Produto.");
                }
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar deletar produtos. Erro: {ex.Message}");
            }
        }

        [HttpPut("desativar/{produtoId}")]
        public async Task<IActionResult> DisableProduto(int produtoId)
        {
            try
            {
                var produto = await _produtoService.UpdateStatusProduto(produtoId, false);

                if (produto) return Ok("Produto Desativado.");

                return BadRequest("Erro ao Desativar o produto.");
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar desativar o produto. Erro: {ex.Message}");
            }
        }

        [HttpPut("ativar/{produtoId}")]
        public async Task<IActionResult> EnableProduto(int produtoId)
        {
            try
            {
                var produto = await _produtoService.UpdateStatusProduto(produtoId, true);

                if (produto) return Ok("Produto Ativado.");

                return BadRequest("Erro ao Ativar o produto.");
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar ativar o produto. Erro: {ex.Message}");
            }
        }
    }
}
