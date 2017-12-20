using LearningPlatform.Data.EntityFramework.BaseRepository;
using LearningPlatform.Domain.Constants;
using LearningPlatform.Domain.SurveyDesign;
using LearningPlatform.Domain.SurveyDesign.RepositoryContracts;
using LearningPlatform.Domain.SurveyDesign.RepositoryContracts.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace LearningPlatform.Data.EntityFramework.Repository
{
    internal class SurveyRepository : ISurveyRepository
    {
        private readonly GenericRepository<Survey> _genericRepository;

        public SurveyRepository(GenericRepository<Survey> genericRepository)
        {
            _genericRepository = genericRepository;
        }

        private DatabaseContext.SurveyContext Context => _genericRepository.Context;

        public void Add(Survey survey)
        {
            _genericRepository.Add(survey);
        }
        public void Update(Survey survey)
        {
            _genericRepository.Update(survey);
        }

        public Survey GetById(long surveyId)
        {
            Survey survey = Context.Surveys
                .FirstOrDefault(p => p.Id == surveyId);

            return survey;
        }

        public Survey GetWithAllRowVersionsOfPages(long surveyId)
        {
            return Context.Surveys
                .FirstOrDefault(p => p.Id == surveyId);
        }

        public Survey GetSurveyInfoById(long surveyId)
        {
            return Context.Surveys
                .FirstOrDefault(p => p.Id == surveyId);
        }

        public List<Survey> GetAllSurveys()
        {
            return Context.Surveys.OrderByDescending(s => s.Id).ToList();
        }

        public IEnumerable<Survey> SearchSurveys(SurveySearchFilter surveySearchModel, string userId)
        {
            ComparisonOperators sentOperator;

            var query = String.IsNullOrEmpty(userId)
                ? Context.Surveys.OrderByDescending(s => s.Id)
                : Context.Surveys.Where(s => s.Name.Contains(surveySearchModel.SearchString) && s.UserId == userId);

            if (!String.IsNullOrWhiteSpace(surveySearchModel.SearchString))
            {
                query = query.Where(s => s.Name.Contains(surveySearchModel.SearchString));
            }
            if (!string.IsNullOrWhiteSpace(surveySearchModel.CreatedDateOperator))
            {
                Enum.TryParse(surveySearchModel.CreatedDateOperator, out sentOperator);
                var createdDate = ConvertDate(surveySearchModel.CreatedDate);
                var createdDateTo = ConvertDate(surveySearchModel.CreatedDateTo);

                switch (sentOperator)
                {
                    case ComparisonOperators.EQUAL:
                        query = query.Where(p => DbFunctions.TruncateTime(p.Created) == createdDate.Date);
                        break;
                    case ComparisonOperators.LESSTHAN:
                        query = query.Where(p => DbFunctions.TruncateTime(p.Created) < createdDate.Date);
                        break;
                    case ComparisonOperators.GREATERTHAN:
                        query = query.Where(p => DbFunctions.TruncateTime(p.Created) > createdDate.Date);
                        break;
                    case ComparisonOperators.BETWEEN:
                        query = query.Where(p => DbFunctions.TruncateTime(p.Created) >= createdDate.Date && DbFunctions.TruncateTime(p.Created) <= createdDateTo.Date);
                        break;
                }
            }
            if (!string.IsNullOrWhiteSpace(surveySearchModel.ModifiedDateOperator))
            {
                Enum.TryParse(surveySearchModel.ModifiedDateOperator, out sentOperator);
                var modifiedDate = ConvertDate(surveySearchModel.ModifiedDate);
                var modifiedDateTo = ConvertDate(surveySearchModel.ModifiedDateTo);

                switch (sentOperator)
                {
                    case ComparisonOperators.EQUAL:
                        query = query.Where(p => DbFunctions.TruncateTime(p.Modified) == modifiedDate.Date);
                        break;
                    case ComparisonOperators.LESSTHAN:
                        query = query.Where(p => DbFunctions.TruncateTime(p.Modified) < modifiedDate.Date);
                        break;
                    case ComparisonOperators.GREATERTHAN:
                        query = query.Where(p => DbFunctions.TruncateTime(p.Modified) > modifiedDate.Date);
                        break;
                    case ComparisonOperators.BETWEEN:
                        query = query.Where(p => DbFunctions.TruncateTime(p.Modified) >= modifiedDate.Date && DbFunctions.TruncateTime(p.Modified) <= modifiedDateTo.Date);
                        break;
                }
            }

            var listOfStatus = new List<SurveyStatus>();
            if (surveySearchModel.Status.New)
            {
                listOfStatus.Add(SurveyStatus.New);
            }
            if (surveySearchModel.Status.Open)
            {
                listOfStatus.Add(SurveyStatus.Open);
            }
            if (surveySearchModel.Status.Closed)
            {
                listOfStatus.Add(SurveyStatus.Closed);
            }
            if (surveySearchModel.Status.TemprorarilyClosed)
            {
                listOfStatus.Add(SurveyStatus.TemprorarilyClosed);
            }

            return query;

        }
        private DateTime ConvertDate(string value)
        {
            DateTime dateTimeResult;
            return DateTime.TryParse(value, out dateTimeResult) ? dateTimeResult : DateTime.MinValue;
        }
        public IEnumerable<Survey> GetByUserId(string userId)
        {
            return Context.Surveys
                .Where(s => s.UserId == userId)
                .OrderBy(s => s.Name);
        }

        public Survey GetWithoutAllIncludings(long surveyId)
        {
            return Context.Surveys.FirstOrDefault(s => s.Id == surveyId);
        }

        public bool Exists(long surveyId)
        {
            return Context.Surveys.Any(s => s.Id == surveyId);
        }

        public void UpdateLastPublished(long surveyId)
        {
            Survey survey = GetById(surveyId);
            survey.LastPublished = DateTime.Now;
            _genericRepository.Update(survey);
        }
    }
}
