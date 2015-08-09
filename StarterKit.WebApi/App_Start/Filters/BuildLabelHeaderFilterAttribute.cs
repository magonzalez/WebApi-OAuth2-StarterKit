using System.Web.Mvc;

using StarterKit.Framework.Extensions;

namespace StarterKit.WebApi.Filters
{
    public class BuildLabelHeaderFilterAttribute : ActionFilterAttribute, IResultFilter
    {
        public override void OnResultExecuting(ResultExecutingContext filterContext) { }

        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            filterContext.HttpContext.Response.AddHeader("LightSail-Build",
                AssemblyExtensions.GetBuildLabel());
        }
    }
}