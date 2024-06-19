using WebApIRedArbor.Models;

namespace WebApIRedArbor.Data.Contracts
{
    public interface IRepositoryRole
    {
        /// <summary>
        /// Obtienen Todos los registros que estan en la tabla Role
        /// </summary>
        /// <returns>List<Role></returns>
        List<Role> GetAllRole();

        /// <summary>
        /// Obtiene Registro por Id de la Tabla
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Role</returns>
        Role GetRole(int id);

        /// <summary>
        /// Agrega Registro a la tabla Role
        /// </summary>
        /// <param name="objRole"></param>
        /// <returns>Retorna Objeto Registrado</returns>
        Role AddRole(Role objRole);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="objRole"></param>
        /// <returns>Registro Actualizado</returns>
        Role UpdateRole(int id, Role objRole);

        /// <summary>
        /// Borrado Registro 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Mensaje Borrado Exitoso</returns>
        string DeleteRole(int id);

        /// <summary>
        /// Valida que exista el RoleId
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns>Bool</returns>
        bool Exists(int roleId);
    }
}
