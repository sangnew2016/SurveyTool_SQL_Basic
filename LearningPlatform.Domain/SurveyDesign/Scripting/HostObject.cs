using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Autofac;
using LearningPlatform.Domain.Common;

namespace LearningPlatform.Domain.SurveyDesign.Scripting
{
    public class HostObject : IHostObject
    {
        private readonly IRequestContext _requestContext;
        private readonly IComponentContext _componentContext;

        public HostObject(IRequestContext requestContext, IComponentContext componentContext)
        {
            _requestContext = requestContext;
            _componentContext = componentContext;
        }

        //private IQuestionService QuestionService
        //{
        //    get { return _componentContext.Resolve<IQuestionService>(); }
        //}

        //private LoopService LoopService
        //{
        //    get { return _componentContext.Resolve<LoopService>(); }
        //}

        //private RespondentUrlService RespondentUrlService
        //{
        //    get { return _componentContext.Resolve<RespondentUrlService>(); }
        //}

        public string GetTitle(string id)
        {
            return "";
            //return QuestionService.GetQuestion(id).Title;
        }

        public string GetDescription(string id)
        {
            return "";
            //return QuestionService.GetQuestion(id).Description;
        }

        public bool IsForward()
        {
            return _requestContext.IsForward;
        }

        public bool Contains(IList<string> list, string element)
        {
            return list.Contains(element);
        }

        public void Redirect(string url)
        {
            //string surveyLink = RespondentUrlService.GetRedirectSurveyLink(_requestContext);

            var uriBuilder = new UriBuilder(url);
            var query = HttpUtility.ParseQueryString(uriBuilder.Query);
            //query["return"] = surveyLink;
            //uriBuilder.Query = query.ToString();

            //_requestContext.State.RedirectUrl = uriBuilder.ToString();
        }

    }
}
