using WebApIRedArbor.Models;

namespace WebApIRedArbor.Data.Contracts
{
    public interface IRepositoryEmployee
    {
        /// <summary>
        /// Obtiene Arreglo de todos los empleados registrados
        /// </summary>
        /// <returns>Listado de Empleados</returns>
        List<Employee> GetAllEmployees();

        /// <summary>
        /// Obtiene el empleado registrado por Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Retorna Empleado por Id</returns>
        Employee GetEmployee(int id);

        /// <summary>
        /// Registro del Empleado
        /// </summary>
        /// <param name="employee"></param>
        /// <returns>Registro nuevo de Empleado</returns>
        Employee AddEmployee(Employee employee);

        /// <summary>
        /// Actualizacion de un Empleado
        /// </summary>
        /// <param name="id"></param>
        /// <param name="employee"></param>
        /// <returns>Actualizacion del registro Empleado</returns>
        /// <exception cref="Exception"></exception>
        Employee UpdateEmployee(int id, Employee employee);

        /// <summary>
        /// Borrado del registro Empleado
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Borrado de Registro Empleado</returns>
        /// <exception cref="Exception"></exception>
        bool DeleteEmployee(int id);
    }
}
