using WebApIRedArbor.Models;

namespace WebApIRedArbor.Data.Contracts
{
    public interface IRepositoryCompany
    {
        /// <summary>
        /// Obtienen Todos los registros que estan en la tabla Company
        /// </summary>
        /// <returns>List<Company></returns>
        List<Company> GetAllCompany();

        /// <summary>
        /// Obtiene Registro por Id de la Tabla
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Company</returns>
        Company GetCompany(int id);

        /// <summary>
        /// Agrega Registro a la tabla Company
        /// </summary>
        /// <param name="objCompany"></param>
        /// <returns>Retorna Objeto Registrado</returns>
        Company AddCompany(Company objCompany);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="objCompany"></param>
        /// <returns>Registro Actualizado</returns>
        Company UpdateCompany(int id, Company objCompany);

        /// <summary>
        /// Borrado Registro 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Mensaje Borrado Exitoso</returns>
        string DeleteCompany(int id);

        /// <summary>
        /// Valida que Company Id Exista
        /// </summary>
        /// <param name="companyId"></param>
        /// <returns>Bool</returns>
        bool Exists(int companyId);
    }
}
