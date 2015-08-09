using System.Web.Mvc;

using StarterKit.WebApi.Filters;

namespace StarterKit.WebApi
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new BuildLabelHeaderFilterAttribute());
        }
    }
}
