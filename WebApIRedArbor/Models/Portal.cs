using System.ComponentModel.DataAnnotations;

namespace WebApIRedArbor.Models
{
    /// <summary>
    /// Clase Modelo para la tabla Portal
    /// </summary>
    public class Portal
    {
        /// <summary>
        /// Id Rol
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Portal Name
        /// </summary>
        [Required]
        public string? PortalName { get; set; }

        /// <summary>
        /// Estado del Registro
        /// </summary>
        [Required]
        public bool State { get; set; }
    }
}
