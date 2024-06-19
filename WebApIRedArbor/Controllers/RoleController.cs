using Microsoft.AspNetCore.Mvc;
using WebApIRedArbor.Data.Contracts;
using WebApIRedArbor.Functions;
using WebApIRedArbor.Models;

namespace WebApIRedArbor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRepositoryRole data;

        public RoleController(IRepositoryRole data)
        {
            this.data = data;
        }

        /// <summary>
        /// GET: Obtiene Registros de la Tabla Role
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public List<Role> Get()
        {
            return data.GetAllRole();
        }

        /// <summary>
        /// GET: Obtiene Registro de la Tabla Role por ID 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public ApiResponse<object> Get(int id)
        {
            var response = new ApiResponse<object>();
            try
            {
                Role dato = data.GetRole(id);
                response.OperacionExitosa = true;
                response.ValidacionesNegocio = true;                
                response.Mensaje = "Operación exitosa";
                response.Data = dato;
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
        /// POST: Registro de Role
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpPost]
        public ApiResponse<object> Post(Role value)
        {
            var response = new ApiResponse<object>();
            try
            {
                Role dato = data.AddRole(value);
                response.OperacionExitosa = true;
                response.ValidacionesNegocio = true;
                response.Mensaje = "Operación exitosa -- Registro Exitoso";
                response.Data = dato;
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
        /// PUT: Actualizacion registro Role
        /// </summary>
        /// <param name="id"></param>
        /// <param name="Role"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public ApiResponse<object> Update(int id, Role Role)
        {
            var response = new ApiResponse<object>();
            try
            {
                Role dato = data.UpdateRole(id,Role);
                response.OperacionExitosa = true;
                response.ValidacionesNegocio = true;
                response.Mensaje = "Operación exitosa -- Actualizacion Exitosa";
                response.Data = dato;
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
        /// DELETE: Borrado de Registro
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public ApiResponse<object> Delete(int id)
        {
            var response = new ApiResponse<object>();
            try
            {
                string dato = data.DeleteRole(id);
                response.OperacionExitosa = true;
                response.ValidacionesNegocio = true;
                response.Mensaje = dato;
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
