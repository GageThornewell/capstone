using Microsoft.EntityFrameworkCore;
using quikJobs.Data;

public class UserService
{
    private readonly ApplicationDbContext _context;

    public UserService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<ApplicationUser>> GetUsersAsync()
    {
        return await _context.Users.ToListAsync();
    }
}
