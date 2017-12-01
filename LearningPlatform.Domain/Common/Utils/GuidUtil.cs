using System;

namespace LearningPlatform.Domain.Common.Utils
{
    public static class GuidUtil
    {
        public static byte[] GenerateGuidAsByteArray()
        {
            return Guid.NewGuid().ToByteArray();
        }

    }
}
