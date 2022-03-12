using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebApiBiblioteca.Validaciones;

namespace WebApiBiblioteca.Entidades
{
    public class Libros: IValidatableObject
    {
        public int id { get; set; }

        [Required(ErrorMessage = "El campo de {0} es obligatorio")]
        [StringLength(maximumLength:20, ErrorMessage = "El {0} solo puede tener 20 caracteres como maximo")]
        [PrimeraLetraMayuscula]
        public string titulo { get; set; }
        
        public List<Autor> autores { get; set; }

        //[Range(0, 2022, ErrorMessage="El campo de año no esta dentro del rango") ]
        //[NotMapped]
        //public int anio { get; set; }

        
        //[NotMapped]
        //[CreditCard]
        //public string tarjeta { get; set; }

        //[Url]
        //[NotMapped]
        //public string url { get; set; }

        // [Range(10,100)]  validad un valor
        // [NotMapped] atributos no mapeados en la base de datos
        // [CreditCard]  verifica que se cumpla la estructura de una tarjeta de credito
        // 
        
        [NotMapped]
        public int menor { get; set; }
        [NotMapped]
        public int mayor { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {

            if (!string.IsNullOrEmpty(titulo))
            {
                var primeraLetra = titulo[0].ToString();

                if(primeraLetra != primeraLetra.ToUpper())
                {
                    yield return new ValidationResult("La primera letra de ser mayuscula",
                        new String[] { nameof(titulo) });
                }
            }

            if(menor > mayor)
            {
                yield return new ValidationResult("El valor de menor no puede ser mas grande que el valor de mayor!",
                    new String[] { nameof(menor) });
            }

        }
    }
}
