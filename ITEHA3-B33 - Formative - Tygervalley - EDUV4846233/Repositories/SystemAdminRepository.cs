using ITEHA3_B33___Formative___Tygervalley___EDUV4846233.Data;
using ITEHA3_B33___Formative___Tygervalley___EDUV4846233.Models;
using Microsoft.EntityFrameworkCore;

namespace ITEHA3_B33___Formative___Tygervalley___EDUV4846233.Repositories;

public class SystemAdminRepository
{
    private readonly KweziHealthDbContext _context;

    public SystemAdminRepository(KweziHealthDbContext context)
    {
        _context = context;
    }

    public async Task<SystemAdmin?> GetAdminByUsername(string username)
    {
        return await _context.SystemAdmins.FirstOrDefaultAsync(admin => admin.Username == username);
    }
}