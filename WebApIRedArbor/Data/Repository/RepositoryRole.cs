using WebApIRedArbor.Context;
using WebApIRedArbor.Data.Contracts;
using WebApIRedArbor.Models;

namespace WebApIRedArbor.Data.Repository
{
    public class RepositoryRole: IRepositoryRole
    {

        private readonly ConexionSQLServer conexionSQLServer;
        public RepositoryRole(ConexionSQLServer context)
        {
            this.conexionSQLServer = context;
        }

        /// <summary>
        /// Obtienen Todos los registros que estan en la tabla Role
        /// </summary>
        /// <returns>List<Role></returns>
        public List<Role> GetAllRole()
        {
            try
            {
                return conexionSQLServer.Role.ToList();
            }
            catch
            {
                return new List<Role>();
            }
        }

        /// <summary>
        /// Obtiene Registro por Id de la Tabla
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Role</returns>
        public Role GetRole(int id)
        {
            return conexionSQLServer.Role.FirstOrDefault(s => s.Id == id);          
        }

        /// <summary>
        /// Agrega Registro a la tabla Role
        /// </summary>
        /// <param name="objRole"></param>
        /// <returns>Objeto Registrado</returns>
        public Role AddRole(Role objRole)
        {
            Role newRole = new()
            {
                RoleName = objRole.RoleName,
                State = true
            };
            conexionSQLServer.Role.Add(newRole);
            conexionSQLServer.SaveChanges();

            return newRole; ;
        }

        /// <summary>
        /// Actualizacion del Registro Role
        /// </summary>
        /// <param name="id"></param>
        /// <param name="objRole"></param>
        /// <returns>Registro Actualizado</returns>
        /// <exception cref="Exception"></exception>
        public Role UpdateRole(int id, Role objRole)
        {
            var existingRole = conexionSQLServer.Role.FirstOrDefault(s => s.Id == id);
            if (existingRole != null)
            {
                existingRole.RoleName = objRole.RoleName;
                existingRole.State = objRole.State;
                conexionSQLServer.SaveChanges();
                return existingRole;
            }
            else
            {
                throw new Exception($"No se puede actualizar el registro con ID {id} porque no existe.");
            }
        }

        /// <summary>
        /// Borrado Registro 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Mensaje Borrado Exitoso</returns>
        public string DeleteRole(int id)
        {
            var RoleToDelete = conexionSQLServer.Role.FirstOrDefault(s => s.Id == id);
            if (RoleToDelete != null)
            {
                conexionSQLServer.Role.Remove(RoleToDelete);
                conexionSQLServer.SaveChanges();

                return $"El registro con ID {id} ha sido eliminado correctamente.";
            }
            else
            {
                return $"No se puede eliminar el registro con ID {id} porque no existe.";
            }
        }

        /// <summary>
        /// Valida que exista el RoleId
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns>Bool</returns>
        public bool Exists(int roleId)
        {
            return conexionSQLServer.Role.Any(p => p.Id == roleId);
        }

    }
}
