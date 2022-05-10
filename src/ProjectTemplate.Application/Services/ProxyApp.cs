using Orizon.Rest.Chat.Application.Interfaces;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Orizon.Rest.Chat.Application.Services
{
    public class ProxyApp : IProxyApp
    {
        private readonly List<string> insecureProtocols =
            new List<string>() { "up://", "ldap://", "jar://", "gopher://", "mailto://", "ssh2://", "telnet://", "expect://" };

        public async Task<string> PostAsync(string url, HttpRequestMessage request)
        {
            var urlSecure = new StringBuilder();
            insecureProtocols.ForEach(p => RemoveInsecureProtocols(url, p, urlSecure));

            // TODO
            //using (var httpClient = Comunicacao.HttpClientFactory.CreateHttpClient(request))
            //{
            //    var response = await httpClient.PostAsync(urlSecure.ToString(), null);
            //    return await response.Content.ReadAsStringAsync();
            //}

            return null;
        }

        private void RemoveInsecureProtocols(string url, string protocol, StringBuilder urlSecure)
        {
            string value = urlSecure.ToString();
            if (value == string.Empty)
            {
                urlSecure.Append(url.Replace(protocol, string.Empty));
            }
            else
            {
                urlSecure.Clear();
                urlSecure.Append(value.Replace(protocol, string.Empty));
            }
        }
    }
}
