namespace WebApIRedArbor.Functions
{
    public class Util
    {
        /// <summary>
        /// Valiacion si tiene un formato valido un correo
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public static bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
    }
}
