using LearningPlatform.Application.Models;
using LearningPlatform.Application.SurveyDesign;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace LearningPlatform.Api.Controllers
{
    [RoutePrefix("api/surveys/{surveyId:long}")]
    public class SurveysController : ApiController
    {
        private readonly SurveyDefinitionAppService _surveyDefinitionAppService;

        public SurveysController(SurveyDefinitionAppService surveyDefinitionAppService) {
            _surveyDefinitionAppService = surveyDefinitionAppService;
        }

        [Route("")]
        public HttpResponseMessage Get(long surveyId)
        {
            var survey = _surveyDefinitionAppService.GetSurvey(surveyId);
            return Request.CreateResponse(HttpStatusCode.OK, survey);
        }

        [Route("")]
        public IHttpActionResult Post([FromBody] SurveyViewModel surveyModel)
        {
            var survey = _surveyDefinitionAppService.CreateSurvey(surveyModel);
            return Ok(new SurveyInfoVersionModel
            {
                RowVersion = survey.RowVersion,
                SurveyId = survey.Id
            });
        }

        [Route("")]
        [HttpPut]
        public IHttpActionResult Put([FromBody] SurveyViewModel surveyModel)
        {
            var survey = _surveyDefinitionAppService.UpdateSurvey(surveyModel);

            return Ok(new SurveyInfoVersionModel() {
                RowVersion = survey.RowVersion,
                SurveyId = survey.Id
            });
        }

        [Route("")]
        [HttpDelete]
        public IHttpActionResult Delete(long surveyId)
        {
            var survey = _surveyDefinitionAppService.GetSurveyInfoById(surveyId);
            _surveyDefinitionAppService.DeleteSurvey(survey);

            return Ok(new SurveyInfoVersionModel() {
                RowVersion = survey.RowVersion,
                SurveyId = surveyId
            });
        }
    }
}
