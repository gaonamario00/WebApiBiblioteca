using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApiBiblioteca.Entidades
{
    public class Libros
    {
        public int id { get; set; }

        [Required(ErrorMessage = "El campo de {0} es obligatorio")]
        [StringLength(maximumLength:20, ErrorMessage = "El {0} solo puede tener 20 caracteres como maximo")]
        public string titulo { get; set; }
        
        public List<Autor> autores { get; set; }

        [Range(0, 2022, ErrorMessage="El campo de año no esta dentro del rango") ]
        [NotMapped]
        public int anio { get; set; }

        
        [NotMapped]
        [CreditCard]
        public string tarjeta { get; set; }

        [Url]
        [NotMapped]
        public string url { get; set; }

        // [Range(10,100)]  validad un valor
        // [NotMapped] atributos no mapeados en la base de datos
        // [CreditCard]  verifica que se cumpla la estructura de una tarjeta de credito
        // 
    }
}
