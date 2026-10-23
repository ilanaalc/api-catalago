using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CatalagoAPI.Data.Context;
using CatalagoAPI.Models;

namespace CatalagoAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CategoriasController : ControllerBase
    {

        private readonly AplicationDbContext _context;

        public CategoriasController(AplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
         public ActionResult<IEnumerable<Categoria>> Get()
        {
            var categoria = _context.Categorias.ToList();
            if(categoria == null)
            {
                return NotFound("As categorias não foram encontradas");
            }

            return categoria;
        }

        [HttpGet("{id:int}", Name="ObterCategoria")]
        public ActionResult<Categoria> Get(int id)
        {
            var categoria = _context.Categorias.FirstOrDefault(c => c.CategoriaId == id);

            if(categoria == null)
            {
                return NotFound("A categoria informada não foi encontrada");
            }

            return categoria;
        }

        [HttpPost]
        public ActionResult Post(Categoria categoria)
        {
            if(categoria is null)
            {
                return BadRequest();
            }

            _context.Categorias.Add(categoria);
            _context.SaveChanges();

            return new CreatedAtRouteResult("ObterCategoria", 
                new { id = categoria.CategoriaId }, categoria);
        }

    }
}
