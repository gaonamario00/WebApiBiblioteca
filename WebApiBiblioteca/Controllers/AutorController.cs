using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiBiblioteca.Entidades;

namespace WebApiBiblioteca.Controllers
{
    [ApiController]
    [Route("api/autores")]
    public class AutorController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;

        public AutorController(ApplicationDbContext context)
        {
            this.dbContext = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Autor>>> GetAll()
        {
            return await dbContext.autores.ToListAsync();
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Autor>> GetById(int id)
        {
            return await dbContext.autores.FirstOrDefaultAsync(x => x.id == id);
        }

        [HttpPost]
        public async Task<ActionResult> Post(Autor autor)
        {

            var existeLibro = await dbContext.libros.AnyAsync(x=>x.id == autor.libroId);

            if (!existeLibro)
            {
                return BadRequest($"No existe el libro con el id: {autor.libroId}");
            }

            dbContext.Add(autor);
            await dbContext.SaveChangesAsync();

            return Ok();
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(Autor autor, int id)
        {

            var exist = await dbContext.autores.AnyAsync(x=>x.id == id);

            if (!exist)
            {
                return NotFound("La clase especifica no existe");
            }

            if(autor.id != id)
            {
                return BadRequest("El id de la clase no coincide con el establecido con el url");
            }

            dbContext.Update(autor);
            await dbContext.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var exist = await dbContext.autores.AnyAsync(x=>x.id == id);
            if (!exist)
            {
                return NotFound("el recurdos no fue encontrado");
            }

            dbContext.Remove(new Autor { id = id});
            await dbContext.SaveChangesAsync();
            return Ok();
        }

    }
}
