using WebApIRedArbor.Context;
using WebApIRedArbor.Data.Contracts;
using WebApIRedArbor.Models;

namespace WebApIRedArbor.Data.Repository
{
    public class RepositoryPortal: IRepositoryPortal
    {

        private readonly ConexionSQLServer conexionSQLServer;
        public RepositoryPortal(ConexionSQLServer context)
        {
            this.conexionSQLServer = context;
        }

        /// <summary>
        /// Obtienen Todos los registros que estan en la tabla Portal
        /// </summary>
        /// <returns>List<Portal></returns>
        public List<Portal> GetAllPortal()
        {
            try
            {
                return conexionSQLServer.Portal.ToList();
            }
            catch
            {
                return new List<Portal>();
            }
        }

        /// <summary>
        /// Obtiene Registro por Id de la Tabla
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Portal</returns>
        public Portal GetPortal(int id)
        {
            return conexionSQLServer.Portal.FirstOrDefault(s => s.Id == id);          
        }

        /// <summary>
        /// Agrega Registro a la tabla Portal
        /// </summary>
        /// <param name="objPortal"></param>
        /// <returns>Objeto Registrado</returns>
        public Portal AddPortal(Portal objPortal)
        {
            Portal newPortal = new()
            {
                PortalName = objPortal.PortalName,
                State = true
            };
            conexionSQLServer.Portal.Add(newPortal);
            conexionSQLServer.SaveChanges();

            return newPortal; ;
        }

        /// <summary>
        /// Actualizacion del Registro Portal
        /// </summary>
        /// <param name="id"></param>
        /// <param name="objPortal"></param>
        /// <returns>Registro Actualizado</returns>
        /// <exception cref="Exception"></exception>
        public Portal UpdatePortal(int id, Portal objPortal)
        {
            var existingPortal = conexionSQLServer.Portal.FirstOrDefault(s => s.Id == id);
            if (existingPortal != null)
            {
                existingPortal.PortalName = objPortal.PortalName;
                existingPortal.State = objPortal.State;
                conexionSQLServer.SaveChanges();
                return existingPortal;
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
        public string DeletePortal(int id)
        {
            var PortalToDelete = conexionSQLServer.Portal.FirstOrDefault(s => s.Id == id);
            if (PortalToDelete != null)
            {
                conexionSQLServer.Portal.Remove(PortalToDelete);
                conexionSQLServer.SaveChanges();

                return $"El registro con ID {id} ha sido eliminado correctamente.";
            }
            else
            {
                return $"No se puede eliminar el registro con ID {id} porque no existe.";
            }
        }

    }
}
