using ShelterServiceClient.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ShelterServiceClient.Utilities
{
    public interface IHttpRequests
    {
        Task<HttpResponseMessage> PostAsync(string url, Dictionary<string, string>? queries, HttpContent? content, Dictionary<string, string>? headers);
    }

    public class HttpRequests : IHttpRequests
    {
        private readonly IHttpClientFactory _httpClient;

        public HttpRequests(IHttpClientFactory httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<HttpResponseMessage> PostAsync(string url, Dictionary<string, string>? queries, HttpContent? content, Dictionary<string, string>? headers)
        {
            var client = _httpClient.CreateClient();

            if (headers != null)
                foreach (var header in headers)
                    client.DefaultRequestHeaders.Add(header.Key, header.Value);

            url += "?";

            if (queries != null)
                foreach (var query in queries)
                    url += $"{query.Key}={query.Value}&";

            HttpResponseMessage response = await client.PostAsync(url, content).ConfigureAwait(false);

            if (response.StatusCode == HttpStatusCode.Unauthorized) throw new UserUnauthorizedException();
            if (response.StatusCode == HttpStatusCode.Forbidden) throw new UserForbidException();
            if (response.StatusCode == HttpStatusCode.BadRequest) throw new BadRequestException();
            if (response.StatusCode == HttpStatusCode.UnprocessableEntity) throw new UnprocessableEntityException();

            if (response.IsSuccessStatusCode) return response;

            throw new UnknownPostException(response.Content.ToString());
        }
    }
}
