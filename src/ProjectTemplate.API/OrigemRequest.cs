using Microsoft.AspNetCore.Http;
using Orizon.Rest.Chat.API.Token;
using System.Linq;

namespace Orizon.Rest.Chat.API
{
    public static class OrigemRequest
    {
        public readonly static string Auditor = "AUD";
        public readonly static string Prestador = "PRE";

        public static bool TryGetOrigemRequest(this IHttpContextAccessor request, out string origem)
        {
            origem = string.Empty;

            if (string.IsNullOrWhiteSpace(request.HttpContext.Request.Headers["Authorization"].FirstOrDefault()))
                return false;

            try
            {
                var httpRequestMessage = RequestTranscriptHelpers.ToHttpRequestMessage(request.HttpContext.Request);
                var token = Token.Token.RetornaToken(httpRequestMessage);
                origem = string.IsNullOrEmpty(token.idPrestador?.Trim())
                    ? Auditor
                    : Prestador;

                return true;
            }
            catch
            {
                return false;
            }
        }
    }

}
