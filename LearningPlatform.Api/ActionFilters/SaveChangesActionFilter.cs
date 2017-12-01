using LearningPlatform.Data.EntityFramework.DatabaseContext;
using System.Web.Http.Filters;

namespace LearningPlatform.Api.ActionFilters
{
    public class SaveChangesActionFilter : ActionFilterAttribute
    {
        private readonly ContextManagement _contextService;
        public SaveChangesActionFilter(ContextManagement contextService)
        {
            _contextService = contextService;
        }

        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            if (actionExecutedContext.Exception == null && _contextService.HasChanges)
            {
                _contextService.SaveChanges();
            }
        }

    }
}