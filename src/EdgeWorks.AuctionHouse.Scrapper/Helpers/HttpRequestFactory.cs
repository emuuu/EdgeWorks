using EdgeWorks.Shared.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace EdgeWorks.AuctionHouse.Scraper.Helpers
{
    public static class HttpRequestFactoryExtended
    {
        public const long TicksPerSecond = 10000000;

        public static async Task<HttpResponseMessage> Upload(
           string bearerToken, string requestUri, IFormFile file, bool longTimeout = false)
        {
            var builder = new HttpRequestBuilder()
                                .AddMethod(new HttpMethod("POST"))
                                .AddBearerToken(bearerToken)
                                .AddRequestUri(requestUri);

            if (longTimeout)
            {
                builder.AddTimeout(new System.TimeSpan(TicksPerSecond * 300));
            }

            byte[] data;
            using (var br = new BinaryReader(file.OpenReadStream()))
                data = br.ReadBytes((int)file.OpenReadStream().Length);

            ByteArrayContent bytes = new ByteArrayContent(data);


            MultipartFormDataContent multiContent = new MultipartFormDataContent();

            //multiContent.Add(bytes, file.Name, file.FileName);
            multiContent.Add(bytes, "file", file.FileName);

            builder.AddContent(multiContent);

            return await builder.SendAsync();
        }

        public static async Task<HttpResponseMessage> Upload(
   string bearerToken, string requestUri, FileStreamResult file, bool longTimeout = false)
        {
            var builder = new HttpRequestBuilder()
                                .AddMethod(new HttpMethod("POST"))
                                .AddBearerToken(bearerToken)
                                .AddRequestUri(requestUri);

            if (longTimeout)
            {
                builder.AddTimeout(new System.TimeSpan(TicksPerSecond * 300));
            }

            byte[] data;
            using (var br = new BinaryReader(file.FileStream))
                data = br.ReadBytes((int)file.FileStream.Length);

            ByteArrayContent bytes = new ByteArrayContent(data);


            MultipartFormDataContent multiContent = new MultipartFormDataContent();

            //multiContent.Add(bytes, file.FileDownloadName, file.FileDownloadName);
            multiContent.Add(bytes, "file", file.FileDownloadName);
            builder.AddContent(multiContent);

            return await builder.SendAsync();
        }
    }
}
