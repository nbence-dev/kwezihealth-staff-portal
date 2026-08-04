using ITEHA3_B33___Formative___Tygervalley___EDUV4846233.Models;
using Microsoft.EntityFrameworkCore;

namespace ITEHA3_B33___Formative___Tygervalley___EDUV4846233.Data;

public class KweziHealthDbContext : DbContext
{
    public KweziHealthDbContext(DbContextOptions<KweziHealthDbContext> options ) :base(options)
    {
        
    }
    public DbSet<StaffMember> StaffMembers { get; set; }
    public DbSet<SystemAdmin> SystemAdmins { get; set; }
}