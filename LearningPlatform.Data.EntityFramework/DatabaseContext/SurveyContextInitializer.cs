using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningPlatform.Data.EntityFramework.DatabaseContext
{
    internal class SurveyContextInitializer: MigrateDatabaseToLatestVersion<SurveyContext, SurveyContextConfiguration>
    {
    }
}
