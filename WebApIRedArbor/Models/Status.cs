using System.ComponentModel.DataAnnotations;

namespace WebApIRedArbor.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class Status
    {
        /// <summary>
        /// Id Status
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Status Name
        /// </summary>
        [Required]
        public string StatusName { get; set; }

        /// <summary>
        /// Estado del Registro
        /// </summary>
        [Required]
        public bool State { get; set; }
    }
}
