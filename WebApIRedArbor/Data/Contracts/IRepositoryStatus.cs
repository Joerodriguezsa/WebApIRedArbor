using WebApIRedArbor.Models;

namespace WebApIRedArbor.Data.Contracts
{
    public interface IRepositoryStatus
    {
        /// <summary>
        /// Obtienen Todos los registros que estan en la tabla Status
        /// </summary>
        /// <returns>List<Status></returns>
        List<Status> GetAllStatus();

        /// <summary>
        /// Obtiene Registro por Id de la Tabla
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Status</returns>
        Status GetStatus(int id);

        /// <summary>
        /// Agrega Registro a la tabla Status
        /// </summary>
        /// <param name="objStatus"></param>
        /// <returns>Retorna Objeto Registrado</returns>
        Status AddStatus(Status objStatus);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="objStatus"></param>
        /// <returns>Registro Actualizado</returns>
        Status UpdateStatus(int id, Status objStatus);

        /// <summary>
        /// Borrado Registro 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Mensaje Borrado Exitoso</returns>
        string DeleteStatus(int id);
    }
}
