namespace WebApIRedArbor.Functions
{
    /// <summary>
    /// Modelo para Respuesta API
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ApiResponse<T>
    {
        public bool OperacionExitosa { get; set; }
        public bool ValidacionesNegocio { get; set; }
        public string Mensaje { get; set; }
        public T Data { get; set; }
    }
}
