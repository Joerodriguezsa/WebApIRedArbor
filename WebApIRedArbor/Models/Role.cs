using System.ComponentModel.DataAnnotations;

namespace WebApIRedArbor.Models
{
    /// <summary>
    /// Clase Modelo para la tabla Role
    /// </summary>
    public class Role
    {
        /// <summary>
        /// Id Role
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Role Name
        /// </summary>
        [Required]
        public string? RoleName { get; set; }

        /// <summary>
        /// Estado del Registro
        /// </summary>
        [Required]
        public bool State { get; set; }
    }
}
