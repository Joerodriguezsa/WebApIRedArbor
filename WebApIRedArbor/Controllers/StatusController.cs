﻿using Microsoft.AspNetCore.Mvc;
using WebApIRedArbor.Data.Contracts;
using WebApIRedArbor.Functions;
using WebApIRedArbor.Models;

namespace WebApIRedArbor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatusController : ControllerBase
    {
        private readonly IRepositoryStatus data;

        public StatusController(IRepositoryStatus data)
        {
            this.data = data;
        }

        /// <summary>
        /// GET: Obtiene Registros de la Tabla Status
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public List<Status> Get()
        {
            return data.GetAllStatus();
        }

        /// <summary>
        /// GET: Obtiene Registro de la Tabla Status por ID 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public ApiResponse<object> Get(int id)
        {
            var response = new ApiResponse<object>();
            try
            {
                Status dato = data.GetStatus(id);
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
        /// POST: Registro de Status
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpPost]
        public ApiResponse<object> Post(Status value)
        {
            var response = new ApiResponse<object>();
            try
            {
                Status dato = data.AddStatus(value);
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
        /// PUT: Actualizacion registro Status
        /// </summary>
        /// <param name="id"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public ApiResponse<object> Update(int id, Status status)
        {
            var response = new ApiResponse<object>();
            try
            {
                Status dato = data.UpdateStatus(id,status);
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
                string dato = data.DeleteStatus(id);
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
