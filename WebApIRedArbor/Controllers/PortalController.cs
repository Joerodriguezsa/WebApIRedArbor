using Microsoft.AspNetCore.Mvc;
using WebApIRedArbor.Data.Contracts;
using WebApIRedArbor.Functions;
using WebApIRedArbor.Models;

namespace WebApIRedArbor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PortalController : ControllerBase
    {
        private readonly IRepositoryPortal data;

        public PortalController(IRepositoryPortal data)
        {
            this.data = data;
        }

        /// <summary>
        /// GET: Obtiene Registros de la Tabla Portal
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public List<Portal> Get()
        {
            return data.GetAllPortal();
        }

        /// <summary>
        /// GET: Obtiene Registro de la Tabla Portal por ID 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public ApiResponse<object> Get(int id)
        {
            var response = new ApiResponse<object>();
            try
            {
                Portal dato = data.GetPortal(id);
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
        /// POST: Registro de Portal
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpPost]
        public ApiResponse<object> Post(Portal value)
        {
            var response = new ApiResponse<object>();
            try
            {
                Portal dato = data.AddPortal(value);
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
        /// PUT: Actualizacion registro Portal
        /// </summary>
        /// <param name="id"></param>
        /// <param name="Portal"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public ApiResponse<object> Update(int id, Portal Portal)
        {
            var response = new ApiResponse<object>();
            try
            {
                Portal dato = data.UpdatePortal(id,Portal);
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
                string dato = data.DeletePortal(id);
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
