using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace WebApIRedArbor.Models
{
    /// <summary>
    /// Clase del Empleado
    /// </summary>
    public class Employee
    {
        /// <summary>
        /// Id de la tabla
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Campo relacionado Compañia Id
        /// </summary>
        public int CompanyId { get; set; }

        /// <summary>
        /// Fecha Registro Empleado
        /// </summary>
        [Required(ErrorMessage = "Campo CreatedOn es requerido")]
        [DataType(DataType.DateTime)]
        public DateTime CreatedOn { get; set; }

        /// <summary>
        /// Fecha Borrado Empleado
        /// </summary>
        [DataType(DataType.DateTime)]
        public DateTime? DeletedOn { get; set; }

        /// <summary>
        /// Email del Empleado
        /// </summary>
        [StringLength(150)]
        public string Email { get; set; }

        /// <summary>
        /// Fax del Empleado
        /// </summary>
        [StringLength(150)]
        public string Fax { get; set; }

        /// <summary>
        /// Nombre del Empleado
        /// </summary>
        [StringLength(150)]
        public string Name { get; set; }

        /// <summary>
        /// Fecha Ultimo Logueo
        /// </summary>
        [DataType(DataType.DateTime)]
        public DateTime? Lastlogin { get; set; }

        /// <summary>
        /// Contraseña del Empleado
        /// </summary>
        [StringLength(150)]
        public string Password { get; set; }

        /// <summary>
        /// Campo Relacionado del Portal Id
        /// </summary>
        public int PortalId { get; set; }

        /// <summary>
        /// Campo Relacionado del Role Id
        /// </summary>
        public int RoleId { get; set; }

        /// <summary>
        /// Campo Relacionado del Status Id
        /// </summary>
        public int StatusId { get; set; }

        /// <summary>
        /// Telefono del Empleado
        /// </summary>
        [StringLength(150)]
        public string Telephone { get; set; }

        /// <summary>
        /// Fecha de Actualizacion del Empleado
        /// </summary>
        [DataType(DataType.DateTime)]
        public DateTime? UpdatedOn { get; set; }

        /// <summary>
        /// Campo UserName del Empleado
        /// </summary>
        [StringLength(150)]
        public string Username { get; set; }
    }

}
