using Alarms.Logic.Models;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text.Encodings.Web;

namespace Alarms.Web.Helper
{
    public static class HTMLExtensions
    {
        public static IHtmlContent SortHeader(this IHtmlHelper htmlHelper,
          string text,
          [AspMvcAction] string action,
          [AspMvcController] string controller,
          string field,
          //string sortField, 
          //string sortOrder,
          IQueryCollection queryString,
          IDictionary<string, object> htmlAttributes)
        {
            var routeValueDictionary = ToRouteValueDictionary(queryString);

            if (
                routeValueDictionary.ContainsKey("sortfield") &&
                routeValueDictionary.ContainsKey("sortorder") &&
                routeValueDictionary["sortfield"].ToString() == field &&
                routeValueDictionary["sortorder"].ToString() == "desc"
            )
            {
                routeValueDictionary.Remove("sortorder");
                routeValueDictionary.Add("sortorder", "asc");
            }
            else
            {
                routeValueDictionary.Remove("sortfield");
                routeValueDictionary.Add("sortfield", field);

                routeValueDictionary.Remove("sortorder");
                routeValueDictionary.Add("sortorder", "desc");
            }

            return htmlHelper.ActionLink(text, action, controller, routeValueDictionary, htmlAttributes);
        }

        public static IHtmlContent SortIdentifier(this IHtmlHelper htmlHelper, string field,
            IQueryCollection queryString)
        {
            var routeValueDictionary = ToRouteValueDictionary(queryString);

            if (!routeValueDictionary.ContainsKey("sortfield") || !routeValueDictionary.ContainsKey("sortorder"))
            {
                return null;
            }

            var sortField = routeValueDictionary["sortfield"].ToString();
            var sortOrder = routeValueDictionary["sortorder"].ToString();

            if (field != sortField)
            {
                return null;
            }

            var glyph = "bi-caret-up-fill";

            if (sortOrder.ToLower().Contains("desc"))
            {
                glyph = "bi-caret-down-fill";
            }

            var icon = new TagBuilder("i");
            icon.Attributes["class"] = glyph;

            return htmlHelper.Raw(GetStringFromHtmlContent(icon));
        }

        public static IHtmlContent SortIdentifier(this IHtmlHelper htmlHelper, string sortOrder, string field)
        {
            if (string.IsNullOrEmpty(sortOrder) || sortOrder.Trim() != field
                && sortOrder.Replace("_desc", string.Empty).Trim() != field)
            {
                return null;
            }

            var glyph = "bi-caret-up-fill";

            if (sortOrder.ToLower().Contains("desc"))
            {
                glyph = "bi-caret-down-fill";
            }

            var icon = new TagBuilder("i");
            icon.Attributes["class"] = glyph;

            return htmlHelper.Raw(GetStringFromHtmlContent(icon));
        }

        public static IHtmlContent Pagination(this IHtmlHelper htmlHelper,
            IUrlHelper urlHelper,
            [AspMvcAction] string action,
            [AspMvcController] string controller,
            PageInfo pageInfo,
            IQueryCollection queryString,
            IDictionary<string, object> htmlAttributes)
        {
            var routeValueDictionary = ToRouteValueDictionary(queryString);

            if (!routeValueDictionary.ContainsKey("page") || !routeValueDictionary.ContainsKey("pageSize"))
            {
                return null;
            }

            var pager = "<nav aria-label=\"Page navigation\">" +
                        "<ul class=\"pagination\">";

            pager = pager + PagerAction(urlHelper, "First", action, controller, routeValueDictionary, 1,
                htmlAttributes, pageInfo.PageNumber == 1);

            pager = pager + PagerAction(urlHelper, "Previous", action, controller, routeValueDictionary, pageInfo.PageNumber - 1,
                 htmlAttributes, !pageInfo.HasPreviousPage);

            foreach (var page in pageInfo.PagesDictionary())
            {
                pager = pager + PagerAction(urlHelper, page.Value, action, controller, routeValueDictionary, page.Key,
                    htmlAttributes, currentPage: page.Key == pageInfo.PageNumber);
            }

            pager = pager + PagerAction(urlHelper, "Next", action, controller, routeValueDictionary, pageInfo.PageNumber + 1,
                htmlAttributes, !pageInfo.HasNextPage);

            pager = pager + PagerAction(urlHelper, "Last", action, controller, routeValueDictionary, pageInfo.PageCount,
                htmlAttributes, pageInfo.PageNumber == pageInfo.PageCount);

            pager = pager + "</ul></nav>";

            return htmlHelper.Raw(pager);
        }

        public static IHtmlContent Pagination2(this IHtmlHelper htmlHelper,
           IUrlHelper urlHelper,
           [AspMvcAction] string action,
           [AspMvcController] string controller,
           PageInfo pageInfo,
           IQueryCollection queryString,
           IDictionary<string, object> htmlAttributes)
        {
            var routeValueDictionary = ToRouteValueDictionary(queryString);

            /*
            if (!routeValueDictionary.ContainsKey("page") || !routeValueDictionary.ContainsKey("pageSize"))
            {
                return null;
            }
            */

            var pager = "<nav aria-label=\"Page navigation\">" +
                        "<ul class=\"pagination\">";

            pager = pager + PagerAction2(urlHelper, "First", action, controller, queryString, 1,
                htmlAttributes, pageInfo.PageNumber == 1);

            pager = pager + PagerAction2(urlHelper, "Previous", action, controller, queryString, pageInfo.PageNumber - 1,
                 htmlAttributes, !pageInfo.HasPreviousPage);

            foreach (var page in pageInfo.PagesDictionary())
            {
                pager = pager + PagerAction2(urlHelper, page.Value, action, controller, queryString, page.Key,
                    htmlAttributes, currentPage: page.Key == pageInfo.PageNumber);
            }

            pager = pager + PagerAction2(urlHelper, "Next", action, controller, queryString, pageInfo.PageNumber + 1,
                htmlAttributes, !pageInfo.HasNextPage);

            pager = pager + PagerAction2(urlHelper, "Last", action, controller, queryString, pageInfo.PageCount,
                htmlAttributes, pageInfo.PageNumber == pageInfo.PageCount);

            pager = pager + "</ul></nav>";

            return htmlHelper.Raw(pager);
        }

        private static string PagerAction(IUrlHelper urlHelper, string text, string action, string controller, RouteValueDictionary routeValueDictionary, int page, IDictionary<string, object> htmlAttributes, bool disabled = false, bool currentPage = false)
        {
            var linkClass = disabled ? "page-item disabled" : "page-item";

            var url = urlHelper.Action(
                action,
                controller,
                new
                {
                    startDateTime = routeValueDictionary["startDateTime"],
                    endDateTime = routeValueDictionary["endDateTime"],
                    filterdate = routeValueDictionary["filterdate"],
                    statusPointId = routeValueDictionary["statusPointId"],
                    sections = routeValueDictionary["sections"],
                    sortfield = routeValueDictionary["sortfield"],
                    sortorder = routeValueDictionary["sortorder"],
                    page = page,
                    pageSize = routeValueDictionary["pageSize"]
                });

            var pager = $"<li class=\"{linkClass}\">"
                        + $"<a class=\"page-link\""
                        + $" href=\"{url}\" ";

            foreach (var attr in htmlAttributes)
            {
                pager = pager + $" {attr.Key}=\"{attr.Value}\"";
            }

            if (disabled)
            {
                pager = pager + " tabindex=\"-1\" aria-disabled=\"True\"";
            }

            pager = currentPage ? pager + $"> <strong>{text}</strong> </a></li>" : pager + $"> {text} </a></li>";

            return pager;
        }

        private static string PagerAction2(IUrlHelper urlHelper,
            string text,
            string action,
            string controller,
            IQueryCollection routeValueDictionary,
            int page,
            IDictionary<string, object> htmlAttributes,
            bool disabled = false,
            bool currentPage = false)
        {
            var linkClass = disabled ? "page-item disabled" : "page-item";

            var newRouteValues = ToRouteValueDictionary(routeValueDictionary, "page", page.ToString());

            var url = urlHelper.Action(
                action,
                controller,
                newRouteValues);

            var pager = $"<li class=\"{linkClass}\">"
                        + $"<a class=\"page-link\""
                        + $" href=\"{url}\" ";

            foreach (var attr in htmlAttributes)
            {
                pager = pager + $" {attr.Key}=\"{attr.Value}\"";
            }

            if (disabled)
            {
                pager = pager + " tabindex=\"-1\" aria-disabled=\"True\"";
            }

            pager = currentPage ? pager + $"> <strong>{text}</strong> </a></li>" : pager + $"> {text} </a></li>";

            return pager;
        }

        public static RouteValueDictionary ToRouteValueDictionary(
            this IQueryCollection collection,
            string newKey,
            string newValue)
        {
            var routeValueDictionary = new RouteValueDictionary();
            foreach (var key in collection.Keys)
            {
                if (key == null)
                {
                    continue;
                }

                if (routeValueDictionary.ContainsKey(key))
                {
                    routeValueDictionary.Remove(key);
                }

                routeValueDictionary.Add(key, collection[key]);
            }

            if (string.IsNullOrEmpty(newValue))
            {
                routeValueDictionary.Remove(newKey);
            }
            else
            {
                if (routeValueDictionary.ContainsKey(newKey))
                {
                    routeValueDictionary.Remove(newKey);
                }

                routeValueDictionary.Add(newKey, newValue);
            }

            return routeValueDictionary;
        }

        public static RouteValueDictionary ToRouteValueDictionary(
            this IQueryCollection collection,
            Dictionary<string, string> additonalEntries)
        {
            var routeValueDictionary = new RouteValueDictionary();
            foreach (var key in collection.Keys)
            {
                if (key == null)
                {
                    continue;
                }

                if (routeValueDictionary.ContainsKey(key))
                {
                    routeValueDictionary.Remove(key);
                }

                routeValueDictionary.Add(key, collection[key]);
            }

            foreach (var key in additonalEntries.Keys)
            {
                if (string.IsNullOrEmpty(additonalEntries[key]))
                {
                    routeValueDictionary.Remove(key);
                }
                else
                {
                    if (routeValueDictionary.ContainsKey(key))
                    {
                        routeValueDictionary.Remove(key);
                    }

                    routeValueDictionary.Add(key, additonalEntries[key]);
                }
            }

            return routeValueDictionary;
        }

        public static RouteValueDictionary ToRouteValueDictionary(this IQueryCollection collection)
        {
            var routeValueDictionary = new RouteValueDictionary();
            foreach (var key in collection.Keys)
            {
                if (key == null)
                {
                    continue;
                }

                if (routeValueDictionary.ContainsKey(key))
                {
                    routeValueDictionary.Remove(key);
                }

                routeValueDictionary.Add(key, collection[key]);
            }

            return routeValueDictionary;
        }

        private static string GetStringFromHtmlContent(IHtmlContent content)
        {
            using (var writer = new System.IO.StringWriter())
            {
                content.WriteTo(writer, HtmlEncoder.Default);
                return writer.ToString();
            }
        }
    }
}

