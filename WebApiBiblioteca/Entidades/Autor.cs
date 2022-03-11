namespace WebApiBiblioteca.Entidades
{
    public class Autor
    {

        public int id { get; set; }
        public string nombre { get; set; }
        public int libroId { get; set; }
        public Libros libro { get; set; }

    }
}
