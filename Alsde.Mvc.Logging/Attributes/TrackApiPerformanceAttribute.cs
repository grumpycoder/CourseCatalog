using System.Web.Http.Controllers;
using System.Web.Http.Filters;
//using ActionFilterAttribute = System.Web.Http.Filters.ActionFilterAttribute;

namespace Alsde.Mvc.Logging.Attributes
{
    public class TrackApiPerformanceAttribute : ActionFilterAttribute
    {
        private string _productName;
        private string _layerName;

        // can use like [TrackPerformance("ToDos", "Mvc")]
        public TrackApiPerformanceAttribute(string product, string layer)
        {
            _productName = product;
            _layerName = layer;
        }

        public override void OnActionExecuting(HttpActionContext filterContext)
        {
            string userId, userName, location;
            var dict = Helpers.GetWebFloggingData(out userId, out userName, out location);

            var type = filterContext.Request.Method.Method;
            var perfName = filterContext.ActionDescriptor.ActionName + "_" + type;

            var stopwatch = new PerfTracker(perfName, userId, userName, location,
                _productName, _layerName, dict);

            var hasKey = filterContext.Request.Properties.ContainsKey("StopwatchApi");
            filterContext.Request.Properties.Remove("StopwatchApi");

            filterContext.Request.Properties.Add("StopwatchApi", stopwatch);
        }

        public override void OnActionExecuted(HttpActionExecutedContext filterContext)
        {
            filterContext.Request.Properties.TryGetValue("StopwatchApi", out object stopwatch);
            (stopwatch as PerfTracker)?.Stop();

            var hasKey = filterContext.Request.Properties.ContainsKey("StopwatchApi");
            filterContext.Request.Properties.Remove("StopwatchApi");
        }
    }
}
