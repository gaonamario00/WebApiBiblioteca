using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiBiblioteca.Entidades;

namespace WebApiBiblioteca.Controllers
{
    [ApiController]
    [Route("api/libros")]
    public class LibrosController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;
        public LibrosController(ApplicationDbContext context)
        {
            this.dbContext = context;
        }


        /*

        [HttpGet] //   /api/libros
        [HttpGet("listado")] //   /api/libros/listado
        [HttpGet("/listado")] //  /listado
        public async Task<ActionResult<List<Libros>>> Get()
        {
            return await dbContext.libros.Include(x=>x.autores).ToListAsync();
        }
        */
        /*
        [HttpGet]
        public List<Libros> Get()
        {
            return dbContext.libros.Include(x => x.autores).ToList();
        }

        [HttpGet("{id:int}/{titulo=the hunger}")]
        public ActionResult<Libros> Get2(int id, string titulo)
        {
            var libro = dbContext.libros.FirstOrDefault(x => x.id == id);
            if(libro == null)
            {
                return NotFound();
            }
            return libro;
        }

        
        
        [HttpGet("primer")]
        public async Task<ActionResult<Libros>> PrimerLibro()
        {
            return await dbContext.libros.FirstOrDefaultAsync();
        }

        
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Libros>> Get(int id)
        {
            var libros =  await dbContext.libros.FirstOrDefaultAsync(x => x.id == id);

            if(libros == null)
            {
                return NotFound();
            }

            return libros;
        }
        

      
        */

        [HttpGet("{titulo}")]
        public async Task<ActionResult<Libros>> Get( [FromRoute] string titulo)
        {
            var libros = await dbContext.libros.FirstOrDefaultAsync(x => x.titulo.Contains(titulo));
            if (libros == null)
            {
                return NotFound();
            }
            return libros;
        }
  

        [HttpGet("primero")]
        public async Task<ActionResult<Libros>> PrimerLibro([FromHeader] int valor, [FromQuery] string libro, [FromQuery] string libroId)
        {
            return await dbContext.libros.FirstOrDefaultAsync();
        }
        
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Libros libro)
        {
            dbContext.Add(libro);
            await dbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("{id:int}")] //api/libros/1
        public async Task<ActionResult> Put(Libros libro, int id)
        {
            if (libro.id != id)
            {
                return BadRequest("El id del libro no coincide con el establecido en la url");
            }

            dbContext.Update(libro);
            await dbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var exist = await dbContext.libros.AnyAsync(x=>x.id==id);
            if (!exist)
            {
                return NotFound();
            }
            dbContext.Remove(new Libros()
            {
                id = id
            });
            await dbContext.SaveChangesAsync();
            return Ok();
        }

    }
}
