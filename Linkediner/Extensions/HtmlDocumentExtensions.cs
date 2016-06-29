using System;
using HtmlAgilityPack;

namespace Linkediner.Extensions
{
    public static class HtmlDocumentExtensions
    {
        public static string GetQueryString(this HtmlDocument document, string query)
        {
            var result = document.QuerySelector(query);
            return result == null ? string.Empty : result.InnerText;
        }
    }
}