using CatalagoAPI.Data.Context;
using Microsoft.AspNetCore.Mvc;
using CatalagoAPI.Models;

namespace CatalagoAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProdutosController : ControllerBase
    {
        private readonly AplicationDbContext _context; //Injeta uma classe do DbContext

        public ProdutosController(AplicationDbContext context)
        {
            _context = context;
        }

        // Action methods go here
        [HttpGet]
        public ActionResult<IEnumerable<Produto>> Get()
        {
            var produtos = _context.Produtos.ToList(); //Acesso a tabela de produtos
            
            if(produtos is null)
            {
                return NotFound("Produtos não encontrados");
            }

            return produtos;
        }

        [HttpGet("{id:int}")]
        public ActionResult<Produto> Get(int id)
        {
            var produto = _context.Produtos.FirstOrDefault(p => p.ProdutoId == id);
            if(produto == null)
            {
                return NotFound("Produto não encontrado");
            }
            
            return produto;
        }
    }
}
