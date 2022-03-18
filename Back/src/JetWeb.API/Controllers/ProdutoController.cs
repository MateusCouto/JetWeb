using System.Collections.Generic;
using System.Linq;
using JetWeb.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace JetWeb.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventoController : ControllerBase
    {
        public IEnumerable<Produto> _produto = new Produto[] {
            new Produto() {
                ProdutoId = 1,
                Nome = "Tênis Fila Grand Prix Masculino",
                Imagem = "foto.png",
                Descricao = "Cabedal: Têxtil e sintético. Entressola: EVA. Solado: borracha e EVA.",
                Estoque = 1,
                Status = true,
                Preco = 159,

            },
            new Produto() {
                ProdutoId = 2,
                Nome = "Tênis Nike Fly By Mid 3 Masculino - Preto+Branco",
                Imagem = "foto2.png",
                Descricao = "Cabedal: Têxtil com sobreposições texturizadas sem costura acrescentam reforço ao longo da ponta; pontos estratégicos de respirabilidade, calcanhar acolchoado e fecho em cadarço; Entressola: EVA; Solado: Borracha",
                Estoque = 1,
                Status = true,
                Preco = 159,
            }
        };
        public EventoController()
        {
        }

        [HttpGet]
        public IEnumerable<Produto> Get()
        {
            return _produto;
        }

        [HttpGet("{id}")]
        public IEnumerable<Produto> GetById(int id)
        {
            return _produto.Where(produto => produto.ProdutoId == id);
        }

        [HttpPost]
        public string Post()
        {
            return "Exemplo de Post";
        }

        [HttpPut("{id}")]
        public string Put(int id)
        {
            return $"Exemplo de Put com id = {id}";
        }

        [HttpDelete("{id}")]
        public string Delete(int id)
        {
            return $"Exemplo de Delete com id = {id}";
        }
    }
}