using WebApIRedArbor.Context;
using WebApIRedArbor.Data.Contracts;
using WebApIRedArbor.Functions;
using WebApIRedArbor.Models;

namespace WebApIRedArbor.Data.Repository
{
    public class RepositoryEmployee : IRepositoryEmployee
    {        
        private readonly ConexionSQLServer conexionSQLServer;
        private readonly IRepositoryPortal repositoryPortal;
        private readonly IRepositoryCompany repositoryCompany;
        private readonly IRepositoryRole repositoryRole;
        private readonly IRepositoryStatus repositoryStatus;

        public RepositoryEmployee(ConexionSQLServer context, IRepositoryPortal repositoryPortal, IRepositoryCompany repositoryCompany, IRepositoryRole repositoryRole, IRepositoryStatus repositoryStatus)
        {
            this.conexionSQLServer = context;
            this.repositoryPortal = repositoryPortal;
            this.repositoryCompany = repositoryCompany;
            this.repositoryRole = repositoryRole;
            this.repositoryStatus = repositoryStatus;
        }

        /// <summary>
        /// Obtiene Arreglo de todos los empleados registrados
        /// </summary>
        /// <returns>Listado de Empleados</returns>
        public List<Employee> GetAllEmployees()
        {
            return conexionSQLServer.Employee.ToList();
        }

        /// <summary>
        /// Obtiene el empleado registrado por Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Retorna Empleado por Id</returns>
        public Employee GetEmployee(int id)
        {
            return conexionSQLServer.Employee.Find(id);
        }

        /// <summary>
        /// Registro del Empleado
        /// </summary>
        /// <param name="employee"></param>
        /// <returns>Registro nuevo de Empleado</returns>
        public Employee AddEmployee(Employee employee)
        {
            ValidateRequiredFields(employee);
            Employee newEmployee = new Employee
            {
                CompanyId = employee.CompanyId,
                CreatedOn = employee.CreatedOn,
                DeletedOn = employee.DeletedOn,
                Email = employee.Email,
                Fax = employee.Fax,
                Name = employee.Name,
                Lastlogin = employee.Lastlogin,
                Password = employee.Password,
                PortalId = employee.PortalId,
                RoleId = employee.RoleId,
                StatusId = employee.StatusId,
                Telephone = employee.Telephone,
                UpdatedOn = employee.UpdatedOn,
                Username = employee.Username
            };
            conexionSQLServer.Employee.Add(newEmployee);
            conexionSQLServer.SaveChanges();

            return newEmployee;
        }

        /// <summary>
        /// Actualizacion de un Empleado
        /// </summary>
        /// <param name="id"></param>
        /// <param name="employee"></param>
        /// <returns>Actualizacion del registro Empleado</returns>
        /// <exception cref="Exception"></exception>
        public Employee UpdateEmployee(int id, Employee employee)
        {
            ValidateRequiredFields(employee);
            var existingEmployee = conexionSQLServer.Employee.Find(id);
            if (existingEmployee != null)
            {
                existingEmployee.CompanyId = employee.CompanyId;
                existingEmployee.CreatedOn = employee.CreatedOn;
                existingEmployee.DeletedOn = employee.DeletedOn;
                existingEmployee.Email = employee.Email;
                existingEmployee.Fax = employee.Fax;
                existingEmployee.Name = employee.Name;
                existingEmployee.Lastlogin = employee.Lastlogin;
                existingEmployee.Password = employee.Password;
                existingEmployee.PortalId = employee.PortalId;
                existingEmployee.RoleId = employee.RoleId;
                existingEmployee.StatusId = employee.StatusId;
                existingEmployee.Telephone = employee.Telephone;
                existingEmployee.UpdatedOn = employee.UpdatedOn;
                existingEmployee.Username = employee.Username;
                conexionSQLServer.SaveChanges();
                return existingEmployee;
            }
            else
            {
                throw new Exception($"No se puede actualizar el registro con ID {id} porque no existe.");
            }
        }

        /// <summary>
        /// Borrado del registro Empleado
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Borrado de Registro Empleado</returns>
        /// <exception cref="Exception"></exception>
        public bool DeleteEmployee(int id)
        {
            var employee = conexionSQLServer.Employee.Find(id);
            if (employee != null)
            {
                conexionSQLServer.Employee.Remove(employee);
                conexionSQLServer.SaveChanges();
                return true;
            }
            else
            {
                throw new Exception($"No se puede eliminar el registro con ID {id} porque no existe.");
            }
        }

        /// <summary>
        /// Validacion del Objeto Empleado 
        /// </summary>
        /// <param name="employee"></param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        private void ValidateRequiredFields(Employee employee)
        {
            string mensaje = string.Empty;
            bool valida = false;
            if (employee == null)
            {
                throw new ArgumentNullException(nameof(employee), "El empleado no puede ser nulo.");
            }

            if (!repositoryPortal.Exists(employee.PortalId))
            {
                valida = true;
                mensaje += $"El campo PortalId {employee.PortalId} no existe y/o es Obligaotrio. ";
            }

            if (!repositoryCompany.Exists(employee.CompanyId))
            {
                valida = true;
                mensaje += $"El campo CompanyId {employee.CompanyId} no existe y/o es Obligaotrio. ";
            }

            if (!repositoryRole.Exists(employee.RoleId))
            {
                valida = true;
                mensaje += $"El campo RoleId {employee.RoleId} no existe y/o es Obligaotrio. ";
            }

            if (!repositoryStatus.Exists(employee.StatusId))
            {
                valida = true;
                mensaje += $"El campo StatusId {employee.StatusId} no existe y/o es Obligaotrio. ";
            }

            if (employee.Password == null || employee.Password.Trim() == "")
            {
                valida = true;
                mensaje += $"El campo Password es Obligaotrio. ";
            }

            if (employee.Username == null || employee.Username.Trim() == "")
            {
                valida = true;
                mensaje += $"El campo Username es Obligaotrio. ";
            }

            if (employee.Email == null || employee.Email.Trim() == "")
            {
                valida = true;
                mensaje += $"El campo Email es Obligaotrio. ";
            }else if (!Util.IsValidEmail(employee.Email))
            {
                valida = true;
                mensaje += $"El campo Email '{employee.Email}' no tiene un formato válido. ";
            }

            if (valida)
            {
                throw new Exception(mensaje);
            }
        }

    }
}
