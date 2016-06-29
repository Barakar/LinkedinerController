using System;
using HtmlAgilityPack;

namespace Linkediner.Extensions
{
    public static class HtmlNodeExtensions
    {
        public static string GetQueryString(this HtmlNode node, string query)
        {
            var result = node.QuerySelector(query);
            return result == null ? string.Empty : result.InnerText;
        }
    }
}