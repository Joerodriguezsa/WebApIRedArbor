using System.ComponentModel.DataAnnotations;

namespace WebApIRedArbor.Models
{
    /// <summary>
    /// Clase Modelo para la tabla Company
    /// </summary>
    public class Company
    {
        /// <summary>
        /// Id Company
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Company Name
        /// </summary>
        [Required]
        public string? CompanyName { get; set; }

        /// <summary>
        /// Estado del Registro
        /// </summary>
        [Required]
        public bool State { get; set; }
    }
}
