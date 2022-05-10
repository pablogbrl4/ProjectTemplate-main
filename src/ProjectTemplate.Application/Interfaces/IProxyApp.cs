using System.Net.Http;

namespace Orizon.Rest.Chat.Application.Interfaces
{
    public interface IProxyApp
    {
        System.Threading.Tasks.Task<string> PostAsync(string url, HttpRequestMessage request);
    }
}
