using WebApIRedArbor.Context;
using WebApIRedArbor.Data.Contracts;
using WebApIRedArbor.Models;

namespace WebApIRedArbor.Data.Repository
{
    public class RepositoryCompany: IRepositoryCompany
    {

        private readonly ConexionSQLServer conexionSQLServer;
        public RepositoryCompany(ConexionSQLServer context)
        {
            this.conexionSQLServer = context;
        }

        /// <summary>
        /// Obtienen Todos los registros que estan en la tabla Company
        /// </summary>
        /// <returns>List<Company></returns>
        public List<Company> GetAllCompany()
        {
            try
            {
                return conexionSQLServer.Company.ToList();
            }
            catch
            {
                return new List<Company>();
            }
        }

        /// <summary>
        /// Obtiene Registro por Id de la Tabla
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Company</returns>
        public Company GetCompany(int id)
        {
            return conexionSQLServer.Company.FirstOrDefault(s => s.Id == id);          
        }

        /// <summary>
        /// Agrega Registro a la tabla Company
        /// </summary>
        /// <param name="objCompany"></param>
        /// <returns>Objeto Registrado</returns>
        public Company AddCompany(Company objCompany)
        {
            Company newCompany = new()
            {
                CompanyName = objCompany.CompanyName,
                State = true
            };
            conexionSQLServer.Company.Add(newCompany);
            conexionSQLServer.SaveChanges();

            return newCompany; ;
        }

        /// <summary>
        /// Actualizacion del Registro Company
        /// </summary>
        /// <param name="id"></param>
        /// <param name="objCompany"></param>
        /// <returns>Registro Actualizado</returns>
        /// <exception cref="Exception"></exception>
        public Company UpdateCompany(int id, Company objCompany)
        {
            var existingCompany = conexionSQLServer.Company.FirstOrDefault(s => s.Id == id);
            if (existingCompany != null)
            {
                existingCompany.CompanyName = objCompany.CompanyName;
                existingCompany.State = objCompany.State;
                conexionSQLServer.SaveChanges();
                return existingCompany;
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
        public string DeleteCompany(int id)
        {
            var CompanyToDelete = conexionSQLServer.Company.FirstOrDefault(s => s.Id == id);
            if (CompanyToDelete != null)
            {
                conexionSQLServer.Company.Remove(CompanyToDelete);
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
