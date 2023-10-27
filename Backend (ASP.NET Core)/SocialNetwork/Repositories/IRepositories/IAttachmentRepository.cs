namespace SocialNetwork.Repositories.IRepositories
{
    public interface IAttachmentRepository
    {
        Task DeleteAttachmentAsync(int attachmentId);
    }
}
