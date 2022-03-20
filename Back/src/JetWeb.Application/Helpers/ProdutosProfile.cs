using AutoMapper;
using JetWeb.Application.Dtos;
using JetWeb.Domain.Entities;

namespace JetWeb.API.Helpers
{
    public class ProdutosProfile : Profile
    {
        public ProdutosProfile()
        {
            CreateMap<Produto, ProdutoDto>().ReverseMap();
        }
    }
}