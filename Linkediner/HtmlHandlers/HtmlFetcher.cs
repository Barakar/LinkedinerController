using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using HtmlAgilityPack;
using Linkediner.Interfaces;

namespace Linkediner.HtmlHandlers
{
    public class HtmlFetcher : IHtmlFetcher
    {
        private HttpClient _webClient;

        /// <summary>
        /// Init the default WebClient 
        /// </summary>
        public HtmlFetcher()
        {
            var autoDecompressionHandler = new HttpClientHandler
            {
                AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate
            };

            _webClient = new HttpClient(autoDecompressionHandler);
        }

        /// <summary>
        /// for test manners.
        /// </summary>
        /// <param name="webClient"></param>
        public HtmlFetcher(HttpClient webClient)
        {
            _webClient = webClient;
        }

        public void AddHeader(string name, string value)
        {
            _webClient.DefaultRequestHeaders.Add(name, value);
        }
        public async Task<HtmlDocument> Fetch(string address)
        {
            var sourceCode = await _webClient.GetStringAsync(address).ConfigureAwait(false);

            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(sourceCode);

            return htmlDocument;
        }
    }
}