using System.Collections.Generic;
using System.Linq;
using JetWeb.API.Data;
using JetWeb.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace JetWeb.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventoController : ControllerBase
    {
        private readonly DataContext _context;
        public EventoController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IEnumerable<Produto> Get()
        {
            return _context.Produtos;
        }

        [HttpGet("{id}")]
        public Produto GetById(int id)
        {
            return _context.Produtos.FirstOrDefault(
                produto => produto.ProdutoId == id
            );
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