using Microsoft.EntityFrameworkCore;
using WebApiBiblioteca.Entidades;

namespace WebApiBiblioteca
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Libros> libros { get; set; }
       
        public DbSet<Autor> autores { get; set; }
    }
}
