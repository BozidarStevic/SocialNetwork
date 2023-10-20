using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using SocialNetwork.Data;
using SocialNetwork.Models;
using SocialNetwork.Repositories.IRepositories;

namespace SocialNetwork.Repositories
{
    public class RateRepository : IRateRepository
    {
        private readonly AppDbContext _context;

        public RateRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Rate> CreateRateAsync(Rate rate)
        {
            await _context.Rates.AddAsync(rate);
            await _context.SaveChangesAsync();
            return rate;
        }
        
        public async Task<List<Rate>> FindRatesByPostIdAsync(int postId)
        {
            return await _context.Rates.Where(r => r.PostId == postId).ToListAsync();
        }
    }
}
