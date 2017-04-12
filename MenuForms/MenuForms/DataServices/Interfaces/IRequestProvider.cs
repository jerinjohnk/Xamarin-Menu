using System.Collections.Generic;
using System.Threading.Tasks;

namespace MenuForms.DataServices.Interfaces
{

    public interface IRequestProvider
    {
        string GetQuery(string uri);

        Task<TResult> GetAsync<TResult>(string uri);

        Task<string> PostAsync(string Url, IList<KeyValuePair<string, string>> parameters);

        Task<TResult> PostAsync<TResult>(string uri, TResult data);

        Task<TResult> PostAsync<TRequest, TResult>(string uri, TRequest data);

        Task<TResult> PutAsync<TResult>(string uri, TResult data);

        Task<TResult> PutAsync<TRequest, TResult>(string uri, TRequest data);

        Task<string> GetRequest(string url);
    }
}
