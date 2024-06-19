using WebApIRedArbor.Context;
using WebApIRedArbor.Data.Contracts;
using WebApIRedArbor.Models;

namespace WebApIRedArbor.Data.Repository
{
    public class RepositoryStatus: IRepositoryStatus
    {

        private readonly ConexionSQLServer conexionSQLServer;
        public RepositoryStatus(ConexionSQLServer context)
        {
            this.conexionSQLServer = context;
        }

        /// <summary>
        /// Obtienen Todos los registros que estan en la tabla Status
        /// </summary>
        /// <returns>List<Status></returns>
        public List<Status> GetAllStatus()
        {
            try
            {
                return conexionSQLServer.Status.ToList();
            }
            catch
            {
                return new List<Status>();
            }
        }

        /// <summary>
        /// Obtiene Registro por Id de la Tabla
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Status</returns>
        public Status GetStatus(int id)
        {
            return conexionSQLServer.Status.FirstOrDefault(s => s.Id == id);          
        }

        /// <summary>
        /// Agrega Registro a la tabla Status
        /// </summary>
        /// <param name="objStatus"></param>
        /// <returns>Objeto Registrado</returns>
        public Status AddStatus(Status objStatus)
        {
            Status newStatus = new()
            {
                StatusName = objStatus.StatusName,
                State = true
            };
            conexionSQLServer.Status.Add(newStatus);
            conexionSQLServer.SaveChanges();

            return newStatus; ;
        }

        /// <summary>
        /// Actualizacion del Registro Status
        /// </summary>
        /// <param name="id"></param>
        /// <param name="objStatus"></param>
        /// <returns>Registro Actualizado</returns>
        /// <exception cref="Exception"></exception>
        public Status UpdateStatus(int id, Status objStatus)
        {
            var existingStatus = conexionSQLServer.Status.FirstOrDefault(s => s.Id == id);
            if (existingStatus != null)
            {
                existingStatus.StatusName = objStatus.StatusName;
                existingStatus.State = objStatus.State;
                conexionSQLServer.SaveChanges();
                return existingStatus;
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
        public string DeleteStatus(int id)
        {
            var statusToDelete = conexionSQLServer.Status.FirstOrDefault(s => s.Id == id);
            if (statusToDelete != null)
            {
                conexionSQLServer.Status.Remove(statusToDelete);
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
