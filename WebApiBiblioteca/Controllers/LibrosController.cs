using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiBiblioteca.Entidades;
using WebApiBiblioteca.Filtros;
using WebApiBiblioteca.Interfaces;

namespace WebApiBiblioteca.Controllers
{

    [ApiController]
    [Route("api/libros")]
    //[Authorize]
    public class LibrosController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IService service;
        private readonly ServiceTransient serviceTransient;
        private readonly ServiceScoped serviceScoped;
        private readonly ServiceSingleton serviceSingleton;
        private readonly ILogger<LibrosController> logger;
        public LibrosController(ApplicationDbContext context, IService service,
            ServiceTransient serviceTransient, ServiceScoped serviceScoped,
            ServiceSingleton serviceSingleton, ILogger<LibrosController> logger)
        {
            this.dbContext = context;
            this.service = service;
            this.serviceTransient = serviceTransient;
            this.serviceScoped = serviceScoped;
            this.serviceSingleton = serviceSingleton;
            this.logger = logger;
        }
        [HttpGet("GUID")]
        [ResponseCache(Duration = 10)]
        [ServiceFilter(typeof(FiltroDeAccion))]
        public ActionResult ObtenerGuid()
        {
            return Ok(new
            {
                LibrosControllerTransient = serviceTransient.guid,
                ServiceA_Transient = service.GetTransient(),
                LibrosControllerScoped = serviceScoped.guid,
                ServiceA_Scoped = service.GetScoped(),
                LibrosControllerSingleton = serviceSingleton.guid,
                ServiceA_Singleton = service.GetSingleton()
            });
        }

        [HttpGet] //   /api/libros
        [HttpGet("listado")] //   /api/libros/listado
        [HttpGet("/listado")] //  /listado
        //[ResponseCache(Duration = 15)]
        //[Authorize]
        public async Task<ActionResult<List<Libros>>> Get()
        {
            // Es importante especificar hasta que nivel se ocupan para que no muestre informacion delicada
            // El appsetting.Development se define el nivel de manejo de logs
            // * Niveles de logs
            // Critical
            // Error
            // Warning
            // Information - Configuration actual
            // Debug
            // Trace
            //throw new NotImplementedException();
            logger.LogInformation("Se obtiene el listado de alumnos");
            logger.LogWarning("Se obtiene el listado de alumnos!");
            service.ejecutarJob();
            return await dbContext.libros.Include(x=>x.autores).ToListAsync();
        }
        
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
        */
        
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Libros>> GetOne(int id)
        {
            var libros =  await dbContext.libros.FirstOrDefaultAsync(x => x.id == id);

            if(libros == null)
            {
                return NotFound();
            }

            logger.LogWarning("Se obtiene el libro con ID: "+id);

            return libros;
        }
        
        
      
       

        [HttpGet("{titulo}")]
        public async Task<ActionResult<Libros>> GetLibroBytitulo( [FromRoute] string titulo)
        {
            var libros = await dbContext.libros.FirstOrDefaultAsync(x => x.titulo.Contains(titulo));
            if (libros == null)
            {
                logger.LogError("No se ha encontrado el registro!!");
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

            var existeLibroMismotitulo = await dbContext.libros.AnyAsync(x => x.titulo == libro.titulo);

            if (existeLibroMismotitulo)
            {
                return BadRequest("Ya existe un libro con el mismo titulo");
            }

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
