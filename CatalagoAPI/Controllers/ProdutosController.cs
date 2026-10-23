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

        [HttpGet("{id:int}", Name="ObterProduto")]
        public ActionResult<Produto> Get(int id)
        {
            var produto = _context.Produtos.FirstOrDefault(p => p.ProdutoId == id);
            if(produto == null)
            {
                return NotFound("Produto não encontrado");
            }
            
            return produto;
        }

        [HttpPost]
        public ActionResult Post(Produto produto)
        {
            if (produto is null)
                return BadRequest();

            _context.Produtos.Add(produto); //O método add vai incluir o produto no contexto
            _context.SaveChanges(); //Persiste os dados na tabela do banco de dados

            //Deve informar o produto criado no cabeçalho location da requiição http
            //nome da rota + o id que foi incluido + objeto
            return new CreatedAtRouteResult("ObterProduto", 
                new { id = produto.ProdutoId }, produto);

        }
    }
}
