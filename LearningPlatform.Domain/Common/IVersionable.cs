namespace LearningPlatform.Domain.Common
{
    public interface IVersionable
    {
        byte[] RowVersion { get; set; }
    }
}
