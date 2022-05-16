using Microsoft.AspNetCore.Http;
using Orizon.Rest.Chat.API.Token;
using Orizon.Rest.Chat.Domain.Enums;
using System.Linq;

namespace Orizon.Rest.Chat.API
{
    public static class OrigemRequest
    {
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
                    ? Origem.Auditor
                    : Origem.Prestador;

                return true;
            }
            catch
            {
                return false;
            }
        }
    }

}
