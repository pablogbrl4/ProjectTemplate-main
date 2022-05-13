using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;

namespace Orizon.Rest.Chat.API.Token
{
    public class Token
    {
        public static AuthorizationToken RetornaToken(HttpRequestMessage Request)
        {
            string value = Request.Headers.Authorization.Parameter.Split('.')[1];
            string decodedString = base64Decode(value);
            AuthorizationToken oAuthorizationToken = JsonSerializer.Deserialize<AuthorizationToken>(decodedString);
            oAuthorizationToken.Bind(Request);
            oAuthorizationToken.Tipo = oAuthorizationToken.PrestadorId > 0 ? AuthorizationToken.TIPO.PRESTADOR : AuthorizationToken.TIPO.OPERADORA;
            return oAuthorizationToken;
        }

        public static string base64Encode(string data)
        {
            try
            {
                byte[] encData_byte = new byte[data.Length];
                encData_byte = Encoding.UTF8.GetBytes(data);
                string encodedData = Convert.ToBase64String(encData_byte);
                return encodedData;
            }
            catch (Exception e)
            {
                throw new Exception("Error in base64Encode" + e.Message);
            }
        }

        public static string base64Decode(string data)
        {
            try
            {
                int len = data.Length % 4;
                if (len > 0) data = data.PadRight(data.Length + (4 - len), '=');

                UTF8Encoding encoder = new UTF8Encoding();
                Decoder utf8Decode = encoder.GetDecoder();

                byte[] todecode_byte = Convert.FromBase64String(data);
                int charCount = utf8Decode.GetCharCount(todecode_byte, 0, todecode_byte.Length);
                char[] decoded_char = new char[charCount];
                utf8Decode.GetChars(todecode_byte, 0, todecode_byte.Length, decoded_char, 0);
                string result = new String(decoded_char);
                return result;
            }
            catch (Exception e)
            {
                throw new Exception("Error in base64Decode" + e.Message);
            }
        }
    }
}
