using System;
using System.IO;

namespace LearningPlatform.Domain.SurveyDesign.Scripting
{
    public class ScriptCodeReader : IScriptCodeReader
    {
        public string GetApiCode()
        {
            var domainPath = ApplicationPath.Replace("LearningPlatform.Api\\bin", "LearningPlatform.Domain");
            return File.ReadAllText(Path.Combine(domainPath, @"SurveyDesign\Scripting\javascriptGlobalFunctions.js"));
        }

        private string ApplicationPath
        {
            get
            {
                if (string.IsNullOrEmpty(AppDomain.CurrentDomain.RelativeSearchPath))
                {
                    return AppDomain.CurrentDomain.BaseDirectory;
                }
                return AppDomain.CurrentDomain.RelativeSearchPath; // For Web
            }
        }
    }
}
