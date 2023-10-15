using Microsoft.EntityFrameworkCore;
using SocialNetwork.Data;
using SocialNetwork.Models;
using SocialNetwork.Repositories.IRepositories;

namespace SocialNetwork.Repositories
{
    public class LabelRepository : ILabelRepository
    {
        private readonly AppDbContext _context;

        public LabelRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Label> GetLabelByIdAsync(int labelId)
        {
            return await _context.Labels.FindAsync(labelId);
        }

        public async Task<Label> GetLabelByNameAsync(string labelName)
        {
            return await _context.Labels.Where(label => label.Name == labelName).FirstOrDefaultAsync();
        }

        public async Task<Label> CreateLabelAsync(Label label)
        {
            await _context.Labels.AddAsync(label);
            await _context.SaveChangesAsync();
            return label;
        }
    }
}
