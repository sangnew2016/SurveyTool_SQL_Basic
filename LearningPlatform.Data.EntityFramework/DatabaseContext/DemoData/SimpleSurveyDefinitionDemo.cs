using LearningPlatform.Domain.Common;
using LearningPlatform.Domain.SurveyDesign;
using LearningPlatform.Domain.SurveyDesign.RepositoryContracts;
using LearningPlatform.Domain.SurveyDesign.Scripting;

namespace LearningPlatform.Data.EntityFramework.DatabaseContext.DemoData
{
    public class SimpleSurveyDefinitionDemo
    {
        private readonly ISurveyRepository _surveyRepository;
        private readonly IRequestObjectProvider<SurveyContext> _surveyContextProvider;
        private readonly SurveyDesign.Factory _surveyDesignFactory;
        private readonly IScriptExecutor _scriptExecutor;

        public SimpleSurveyDefinitionDemo(
            ISurveyRepository surveyRepository,
            IRequestObjectProvider<SurveyContext> surveyContextProvider,
            SurveyDesign.Factory surveyDesignFactory,
            IScriptExecutor scriptExecutor)
        {
            _surveyRepository = surveyRepository;
            _surveyContextProvider = surveyContextProvider;
            _surveyDesignFactory = surveyDesignFactory;
            _scriptExecutor = scriptExecutor;
        }

        public void InsertData()
        {
            const long surveyId = 1;
            if (_surveyRepository.Exists(surveyId)) return;

            //============================
            // EX: use multi language
            //============================
            var create = _surveyDesignFactory.Invoke(surveyId: surveyId, useDatabaseIds: true, language: "en");
            var survey = create.Survey(
                surveyModelName: "Simple Survey",
                userId: "f6e021af-a6a0-4039-83f4-152595b4671a",
                title: new [] {"Simple survey title", "vn::Tieu de khao sat don gian"},
                description: new [] {"Simple survey description", "vn::Mo ta khao sat don gian"});

            //_surveyRepository.Add(survey);
            //_surveyContextProvider.Get().SaveChanges();

            //============================
            // EX: javascript inside C#
            // 1. use in .ToString()
            // 2. use directly by IScriptExecutor

            // isNumeric: ham duoc dinh nghia trong javascriptGlobalFunctions.js - ham thuan
            // redirect: ham duoc dinh nghia trong javascriptGlobalFunctions.js
            //      diem dac biet: ham nay co su dung doi tuong IHostObject va phuong thuc cua no duoc viet bang C#
            //      khi javascript(V8) truy xuat den phuong thuc nay, C# se duoc thuc thi
            //      do chinh la su tuong tac: javascript - C# (thong qua V8)


            // 3. JavascriptPropertyBag ke thua tu IPropertyBag (dang mot dictionary)
            //      Muc dich: ep constructor nhan mot func, func se run khi co su truy xuat phan tu trong dictionary
            //      Su dung: ta tu tao cac bien javascript trong app va output cho user xai (vd nhu loops)
            //              create.OpenEndedShortTextQuestion("q2", "Please describe your opinion on {{loops.loop}}")))
            //              Title = CreateLanguageString(title),
            //      Code C# se apply khi bi tuong tac den no
            //============================
            var javascriptCode = "var x = isNumeric(10); x";
            var value = _scriptExecutor.EvaluateCode<bool>(javascriptCode);

            javascriptCode = "redirect('https://google.com.vn')";
            _scriptExecutor.EvaluateCode(javascriptCode);

        }
    }
}
