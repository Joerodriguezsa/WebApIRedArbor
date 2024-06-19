using Microsoft.AspNetCore.Mvc;
using WebApIRedArbor.Data.Contracts;
using WebApIRedArbor.Functions;
using WebApIRedArbor.Models;

namespace WebApIRedArbor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly IRepositoryCompany data;

        public CompanyController(IRepositoryCompany data)
        {
            this.data = data;
        }

        /// <summary>
        /// GET: Obtiene Registros de la Tabla Company
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public List<Company> Get()
        {
            return data.GetAllCompany();
        }

        /// <summary>
        /// GET: Obtiene Registro de la Tabla Company por ID 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public ApiResponse<object> Get(int id)
        {
            var response = new ApiResponse<object>();
            try
            {
                Company dato = data.GetCompany(id);
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
        /// POST: Registro de Company
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpPost]
        public ApiResponse<object> Post(Company value)
        {
            var response = new ApiResponse<object>();
            try
            {
                Company dato = data.AddCompany(value);
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
        /// PUT: Actualizacion registro Company
        /// </summary>
        /// <param name="id"></param>
        /// <param name="Company"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public ApiResponse<object> Update(int id, Company Company)
        {
            var response = new ApiResponse<object>();
            try
            {
                Company dato = data.UpdateCompany(id,Company);
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
                string dato = data.DeleteCompany(id);
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
