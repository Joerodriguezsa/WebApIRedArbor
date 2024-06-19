using Microsoft.AspNetCore.Mvc;
using WebApIRedArbor.Data.Contracts;
using WebApIRedArbor.Functions;
using WebApIRedArbor.Models;

namespace WebApIRedArbor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IRepositoryEmployee _repository;

        public EmployeeController(IRepositoryEmployee repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// GET: Obtiene todos los registros de empleados
        /// </summary>
        /// <returns>Lista de empleados</returns>
        [HttpGet]
        public List<Employee> Get()
        {
            return _repository.GetAllEmployees();
        }

        /// <summary>
        /// GET: Obtiene un registro de empleado por ID
        /// </summary>
        /// <param name="id">ID del empleado</param>
        /// <returns>Empleado</returns>
        [HttpGet("{id}")]
        public ApiResponse<object> Get(int id)
        {
            var response = new ApiResponse<object>();
            try
            {
                var employee = _repository.GetEmployee(id);
                if (employee == null)
                {
                    response.OperacionExitosa = false;
                    response.ValidacionesNegocio = false;
                    response.Mensaje = $"El registro con ID {id} no fue encontrado.";
                }
                else
                {
                    response.OperacionExitosa = true;
                    response.ValidacionesNegocio = true;
                    response.Mensaje = "Operación exitosa";
                    response.Data = employee;
                }
            }
            catch (Exception ex)
            {
                response.OperacionExitosa = false;
                response.ValidacionesNegocio = false;
                response.Mensaje = ex.Message;
            }
            return response;
        }

        /// <summary>
        /// POST: Registra un nuevo empleado
        /// </summary>
        /// <param name="employee">Datos del empleado</param>
        /// <returns>Empleado registrado</returns>
        [HttpPost]
        public ApiResponse<object> Post([FromBody] Employee employee)
        {
            var response = new ApiResponse<object>();
            try
            {
                var newEmployee = _repository.AddEmployee(employee);
                response.OperacionExitosa = true;
                response.ValidacionesNegocio = true;
                response.Mensaje = "Operación exitosa -- Registro Exitoso";
                response.Data = newEmployee;
            }
            catch (Exception ex)
            {
                response.OperacionExitosa = false;
                response.ValidacionesNegocio = false;
                response.Mensaje = ex.Message;
            }
            return response;
        }

        /// <summary>
        /// PUT: Actualiza un registro de empleado
        /// </summary>
        /// <param name="id">ID del empleado</param>
        /// <param name="employee">Datos del empleado</param>
        /// <returns>Empleado actualizado</returns>
        [HttpPut("{id}")]
        public ApiResponse<object> Update(int id, [FromBody] Employee employee)
        {
            var response = new ApiResponse<object>();
            try
            {
                var updatedEmployee = _repository.UpdateEmployee(id, employee);
                response.OperacionExitosa = true;
                response.ValidacionesNegocio = true;
                response.Mensaje = "Operación exitosa -- Actualización Exitosa";
                response.Data = updatedEmployee;
            }
            catch (Exception ex)
            {
                response.OperacionExitosa = false;
                response.ValidacionesNegocio = false;
                response.Mensaje = ex.Message;
            }
            return response;
        }

        /// <summary>
        /// DELETE: Elimina un registro de empleado
        /// </summary>
        /// <param name="id">ID del empleado</param>
        /// <returns>Mensaje de éxito o error</returns>
        [HttpDelete("{id}")]
        public ApiResponse<object> Delete(int id)
        {
            var response = new ApiResponse<object>();
            try
            {
                var result = _repository.DeleteEmployee(id);
                if (result)
                {
                    response.OperacionExitosa = true;
                    response.ValidacionesNegocio = true;
                    response.Mensaje = $"El registro con ID {id} ha sido eliminado correctamente.";
                }
                else
                {
                    response.OperacionExitosa = false;
                    response.ValidacionesNegocio = false;
                    response.Mensaje = $"El registro con ID {id} no fue encontrado.";
                }
            }
            catch (Exception ex)
            {
                response.OperacionExitosa = false;
                response.ValidacionesNegocio = false;
                response.Mensaje = ex.Message;
            }
            return response;
        }
    }
}
