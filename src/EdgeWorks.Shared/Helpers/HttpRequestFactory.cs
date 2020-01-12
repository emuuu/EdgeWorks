using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace EdgeWorks.Shared.Helpers
{
    public static class HttpRequestFactory
    {
        public const long TicksPerSecond = 10000000;

        public static async Task<HttpResponseMessage> Get(string bearerToken, string requestUri, bool longTimeout = false)
        {
            var builder = new HttpRequestBuilder()
                                .AddMethod(HttpMethod.Get)
                                .AddBearerToken(bearerToken)
                                .AddRequestUri(requestUri);

            if (longTimeout)
            {
                builder.AddTimeout(new System.TimeSpan(TicksPerSecond * 300));
            }

            return await builder.SendAsync();
        }

        public static async Task<HttpResponseMessage> Post(
           string bearerToken, string requestUri, object value = null, bool longTimeout = false)
        {
            var builder = new HttpRequestBuilder()
                                .AddMethod(HttpMethod.Post)
                                .AddBearerToken(bearerToken)
                                .AddRequestUri(requestUri);

            if (longTimeout)
            {
                builder.AddTimeout(new System.TimeSpan(TicksPerSecond * 300));
            }

            if (value != null)
                builder.AddContent(new JsonContent(value));

            return await builder.SendAsync();
        }

        public static async Task<HttpResponseMessage> Put(
           string bearerToken, string requestUri, object value = null, bool longTimeout = false)
        {
            var builder = new HttpRequestBuilder()
                                .AddMethod(HttpMethod.Put)
                                .AddBearerToken(bearerToken)
                                .AddRequestUri(requestUri);

            if (longTimeout)
            {
                builder.AddTimeout(new System.TimeSpan(TicksPerSecond * 300));
            }

            if (value != null)
                builder.AddContent(new JsonContent(value));

            return await builder.SendAsync();
        }

        public static async Task<HttpResponseMessage> Patch(
           string bearerToken, string requestUri, object value = null, bool longTimeout = false)
        {
            var builder = new HttpRequestBuilder()
                                .AddMethod(new HttpMethod("PATCH"))
                                .AddBearerToken(bearerToken)
                                .AddRequestUri(requestUri);

            if (longTimeout)
            {
                builder.AddTimeout(new System.TimeSpan(TicksPerSecond * 300));
            }

            if (value != null)
                builder.AddContent(new JsonContent(value));

            return await builder.SendAsync();
        }

        public static async Task<HttpResponseMessage> Delete(string bearerToken, string requestUri, bool longTimeout = false)
        {
            var builder = new HttpRequestBuilder()
                                .AddMethod(HttpMethod.Delete)
                                .AddBearerToken(bearerToken)
                                .AddRequestUri(requestUri);

            if (longTimeout)
            {
                builder.AddTimeout(new System.TimeSpan(TicksPerSecond * 300));
            }

            return await builder.SendAsync();
        }
    }
}
