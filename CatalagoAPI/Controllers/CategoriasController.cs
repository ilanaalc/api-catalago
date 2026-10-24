using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CatalagoAPI.Data.Context;
using CatalagoAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CatalagoAPI.Controllers
{
    [Route("api/[controller]")]
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
            try
            {
                var categoria = _context.Categorias.ToList();
                if (categoria == null)
                {
                    return NotFound("As categorias não foram encontradas");
                }
                return categoria;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tratar a sua solicitação.");
            }
        }

        [HttpGet("{id:int}", Name = "ObterCategoria")]
        public ActionResult<Categoria> Get(int id)
        {
            var categoria = _context.Categorias.FirstOrDefault(c => c.CategoriaId == id);

            if (categoria == null)
            {
                return NotFound("A categoria informada não foi encontrada");
            }

            return categoria;
        }

        [HttpGet("produtos/{id:int}")]
        public ActionResult<Categoria> GetProdutosCategoriaId(int id)
        {
            var categoria = _context.Categorias.Include(c => c.Produtos).FirstOrDefault(c => c.CategoriaId == id);

            if (categoria == null)
            {
                return NotFound("A categoria informada não foi encontrada");
            }

            return categoria;
        }

        [HttpGet("produtos")]
        public ActionResult<IEnumerable<Categoria>> GetProdutoCategorias()
        {
            return _context.Categorias.Include(p => p.Produtos).ToList();
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

        [HttpPut]
        public ActionResult Put(int id, Categoria categoria)
        {
            if(id != categoria.CategoriaId)
            {
                return NotFound();
            }

            _context.Entry(categoria).State = EntityState.Modified;
            _context.SaveChanges();

            return Ok(categoria);
        }

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            var categoria = _context.Categorias.FirstOrDefault(c => c.CategoriaId == id);
            if(categoria is null)
            {
                return NotFound();
            }

            _context.Categorias.Remove(categoria);
            _context.SaveChanges();

            return Ok();
        }

    }
}
