using System.Threading.Tasks;
using HtmlAgilityPack;

namespace Linkediner.Interfaces
{
    public interface IHtmlFetcher
    {
        Task<HtmlDocument> Fetch(string address);
        void AddHeader(string name,string value);
    }
}