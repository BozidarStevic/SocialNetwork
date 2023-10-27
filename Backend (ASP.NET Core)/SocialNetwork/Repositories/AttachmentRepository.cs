using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using SocialNetwork.Data;
using SocialNetwork.Exceptions;
using SocialNetwork.Repositories.IRepositories;

namespace SocialNetwork.Repositories
{
    public class AttachmentRepository : IAttachmentRepository
    {
        private readonly AppDbContext _context;
        public AttachmentRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task DeleteAttachmentAsync(int attachmentId)
        {
            var attachment = await _context.Attachments.FirstOrDefaultAsync(a => a.Id == attachmentId);

            if (attachment != null)
            {
                _context.Attachments.Remove(attachment);
                await _context.SaveChangesAsync();
                
            }
            else
            {
                throw new NotFoundException("Attachment not found");
            }
        }
    }
}
