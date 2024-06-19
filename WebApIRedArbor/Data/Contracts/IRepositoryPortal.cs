using WebApIRedArbor.Models;

namespace WebApIRedArbor.Data.Contracts
{
    public interface IRepositoryPortal
    {
        /// <summary>
        /// Obtienen Todos los registros que estan en la tabla Portal
        /// </summary>
        /// <returns>List<Portal></returns>
        List<Portal> GetAllPortal();

        /// <summary>
        /// Obtiene Registro por Id de la Tabla
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Portal</returns>
        Portal GetPortal(int id);

        /// <summary>
        /// Agrega Registro a la tabla Portal
        /// </summary>
        /// <param name="objPortal"></param>
        /// <returns>Retorna Objeto Registrado</returns>
        Portal AddPortal(Portal objPortal);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="objPortal"></param>
        /// <returns>Registro Actualizado</returns>
        Portal UpdatePortal(int id, Portal objPortal);

        /// <summary>
        /// Borrado Registro 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Mensaje Borrado Exitoso</returns>
        string DeletePortal(int id);
    }
}
