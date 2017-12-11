using System;
using LearningPlatform.Application.Models;
using LearningPlatform.Domain.Common;
using LearningPlatform.Domain.Common.Utils;
using LearningPlatform.Domain.Constants;
using LearningPlatform.Domain.Exceptions;
using LearningPlatform.Domain.SurveyDesign;
using LearningPlatform.Domain.SurveyDesign.RepositoryContracts;
using System.Collections;

namespace LearningPlatform.Application.SurveyDesign
{
    public class SurveyDefinitionAppService
    {
        private readonly ISurveyRepository _surveyRepository;

        private readonly IUnitOfWorkFactory _unitOfWorkFactory;
        private readonly Domain.SurveyDesign.SurveyDesign.Factory _surveyDesignFactory;
        private readonly SurveyDefinitionService _surveyDefinitionService;

        public SurveyDefinitionAppService(ISurveyRepository surveyRepository,
            IUnitOfWorkFactory unitOfWorkFactory, Domain.SurveyDesign.SurveyDesign.Factory surveyDesignFactory,
            SurveyDefinitionService surveyDefinitionService)
        {
            _surveyRepository = surveyRepository;
            _unitOfWorkFactory = unitOfWorkFactory;
            _surveyDesignFactory = surveyDesignFactory;
            _surveyDefinitionService = surveyDefinitionService;
        }

        public Survey GetSurveyInfoById(long surveyId)
        {
            return _surveyDefinitionService.GetSurveyInfoById(surveyId);
        }

        public Survey UpdateSurveyStatus(long surveyId, byte[] rowVersion, SurveyStatus status)
        {
            Survey survey = _surveyDefinitionService.GetSurveyInfoById(surveyId);
            if (!StructuralComparisons.StructuralEqualityComparer.Equals(survey.RowVersion, rowVersion))
            {
                throw new ConcurrencyException();
            }

            using (var unitOfWork = _unitOfWorkFactory.Create())
            {
                _surveyDefinitionService.UpdateSurveyStatus(survey, status);

                unitOfWork.SavePoint();
                unitOfWork.Commit();
                return survey;
            }
        }

        public Survey GetSurvey(long surveyId)
        {
            Survey survey = _surveyRepository.GetById(surveyId);
            if (survey == null) throw new SurveyNotFoundException($"Survey with id = {surveyId} not found");
            return survey;
        }

        public Survey CreateSurvey(SurveyViewModel surveyModel)
        {
            using (var unitOfWork = _unitOfWorkFactory.Create())
            {
                var userId = Guid.NewGuid().ToString();
                var surveyFactory = _surveyDesignFactory.Invoke(useDatabaseIds: true);
                var survey = surveyFactory.Survey(surveyModel.Name, userId,new[] {surveyModel.Title}, new[] {surveyModel.Description});

                _surveyRepository.Add(survey);
                unitOfWork.SavePoint();

                unitOfWork.Commit();
                return survey;
            }
        }

        public Survey UpdateSurvey(SurveyViewModel surveyModel)
        {
            using (var unitOfWork = _unitOfWorkFactory.Create())
            {
                var survey = _surveyRepository.GetById(surveyModel.SurveyId);
                var surveyFactory = _surveyDesignFactory.Invoke(useDatabaseIds: true);
                survey.Name = surveyModel.Name;
                survey.Title = surveyFactory.CreateLanguageString(surveyModel.Title);
                survey.Description = surveyFactory.CreateLanguageString(surveyModel.Description);

                _surveyRepository.Update(survey);

                unitOfWork.SavePoint();
                unitOfWork.Commit();

                return survey;
            }
        }

        public void DeleteSurvey(Survey surveyInfo)
        {
            using (var unitOfWork = _unitOfWorkFactory.Create())
            {
                surveyInfo.IsDeleted = true;
                _surveyRepository.Update(surveyInfo);

                unitOfWork.SavePoint();
                unitOfWork.Commit();
            }
        }

        public void RestoreSurvey(Survey surveyInfo)
        {
            using (var unitOfWork = _unitOfWorkFactory.Create())
            {
                surveyInfo.IsDeleted = false;
                _surveyRepository.Update(surveyInfo);

                unitOfWork.SavePoint();
                unitOfWork.Commit();
            }
        }
    }
}
