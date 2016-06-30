using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using HtmlAgilityPack;
using Linkediner.Interfaces;

namespace Linkediner.HtmlHandlers
{
    public class HtmlFetcher : IHtmlFetcher
    {
        private readonly HttpClient _httpClient;

        /// <summary>
        /// Init the default WebClient 
        /// </summary>
        public HtmlFetcher()
        {
            var autoDecompressionHandler = new HttpClientHandler
            {
                AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate
            };

            _httpClient = new HttpClient(autoDecompressionHandler);
        }

        /// <summary>
        /// for test manners.
        /// </summary>
        /// <param name="httpClient"></param>
        public HtmlFetcher(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public void AddHeader(string name, string value)
        {
            _httpClient.DefaultRequestHeaders.Add(name, value);
        }
        public async Task<HtmlDocument> Fetch(string address)
        {
            var sourceCode = await _httpClient.GetStringAsync(address).ConfigureAwait(false);

            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(sourceCode);

            return htmlDocument;
        }
    }
}