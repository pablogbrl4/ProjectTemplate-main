using System;
using System.Net.Http;

namespace Orizon.Rest.Chat.Application.Requests
{
    public static class OrigemRequest
    {
        public readonly static string Auditor = "AUD";

        public readonly static string Prestador = "PRE";

        public static bool TryGetOrigemRequest(this HttpRequestMessage request, out string origem)
        {
            origem = string.Empty;

            if (request.Headers.Authorization == null)
                return false;

            try
            {
                // TODO:
                //var token = Token.Token.RetornaToken(request);

                //origem = string.IsNullOrEmpty(token.idPrestador?.Trim()) 
                //    ? OrigemRequest.Auditor 
                //    : OrigemRequest.Prestador;

                return true;
            }
            catch
            {
                return false;
            }
        }
    }

}
