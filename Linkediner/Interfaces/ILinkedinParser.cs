using HtmlAgilityPack;
using Linkediner.Models;

namespace Linkediner.Interfaces
{
    public interface ILinkedinParser
    {
        LinkedinProfile ParseProfile(string id, HtmlDocument document);
    }
}