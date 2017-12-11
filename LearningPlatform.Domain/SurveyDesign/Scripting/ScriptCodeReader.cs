using System;
using System.IO;

namespace LearningPlatform.Domain.SurveyDesign.Scripting
{
    public class ScriptCodeReader : IScriptCodeReader
    {
        public string GetApiCode()
        {
            return File.ReadAllText(Path.Combine(ApplicationPath, @"SurveyDesign\Scripting\javascriptGlobalFunctions.js"));
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
