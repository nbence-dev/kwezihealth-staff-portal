// https://stackoverflow.com/questions/70165802/minimal-web-api-and-seeding-an-in-memory-entity-framework-database

using ITEHA3_B33___Formative___Tygervalley___EDUV4846233.Models;
using Microsoft.EntityFrameworkCore;

namespace ITEHA3_B33___Formative___Tygervalley___EDUV4846233.Data;

public class DataSeeder
{
    private readonly KweziHealthDbContext _dbContext;

    public DataSeeder(KweziHealthDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void Seed()
    {
        
        _dbContext.StaffMembers.AddRange(AddStaffMember());
        _dbContext.SaveChanges();
        _dbContext.SystemAdmins.AddRange(AddSystemAdmin());
        _dbContext.SaveChanges();
        
    }

    private List<StaffMember> AddStaffMember()
    {
        return new List<StaffMember>
        {
            new StaffMember {StaffId = 1, FullName = "John Doe", Email = "johndoe@gmail.com", Position = "Doctor", Unit = 1 },
            new StaffMember {StaffId = 2, FullName = "Jane Smith", Email = "janesmith@gmail.com", Position = "Doctor", Unit = 1 },
            
        };
    }

    private List<SystemAdmin> AddSystemAdmin()
    {
        return new List<SystemAdmin>
        {
            new SystemAdmin { Username = "admin", Password = "admin123" },
        };
    }
}