// Deliverable 2: Staff Management Service Layer — in-memory (EF Core InMemory provider) persistence backing StaffService's CRUD operations.
using ITEHA3_B33___Formative___Tygervalley___EDUV4846233.Data;
using ITEHA3_B33___Formative___Tygervalley___EDUV4846233.Models;
using Microsoft.EntityFrameworkCore;


namespace ITEHA3_B33___Formative___Tygervalley___EDUV4846233.Repositories;

public class StaffRepository
{
    // Needed functionality:
    // Add
    // Retrieve all
    // Retrieve by ID
    // Update
    // Delete
    
    private readonly KweziHealthDbContext _dbContext;
    
    public StaffRepository(KweziHealthDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<StaffMember>> GetAllStaff()
    {
        return await _dbContext.StaffMembers.ToListAsync();
    }

    public async Task AddNewStaff(StaffMember staffMember)
    {
        await _dbContext.StaffMembers.AddAsync(staffMember);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteStaff(int id)
    {
        var staffMember = await FindStaffById(id);
        
        if (staffMember != null)
        {
            _dbContext.StaffMembers.Remove(staffMember);
            await _dbContext.SaveChangesAsync();
        }
    }

    public async Task UpdateStaff(StaffMember staffMember)
    {
        _dbContext.StaffMembers.Update(staffMember);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<StaffMember?> FindStaffById(int id)
    {
        return await _dbContext.StaffMembers.FirstOrDefaultAsync(i => i.StaffId == id);
    }
    

    
}