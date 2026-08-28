// Deliverable 2: Staff Management Service Layer — Add/GetAll/GetById/Update/Delete, independent of controllers, backed by an in-memory (EF Core InMemory) data store via StaffRepository.
// https://learn.microsoft.com/en-us/aspnet/mvc/overview/older-versions-1/models-data/validating-with-a-service-layer-cs

using ITEHA3_B33___Formative___Tygervalley___EDUV4846233.DTOs;
using ITEHA3_B33___Formative___Tygervalley___EDUV4846233.Models;
using ITEHA3_B33___Formative___Tygervalley___EDUV4846233.Repositories;

namespace ITEHA3_B33___Formative___Tygervalley___EDUV4846233.Services;

public class StaffService
{
    private readonly StaffRepository _repository;
    // Service layer responsibility: interact with repository layer and perform business logic, validation, etc.
    // Think what each task/function will need and return

    public StaffService(StaffRepository repository)
    {
        _repository = repository;
    }
    
    // Return staff object?
    public async Task AddStaffMember(StaffMember staffMember)
    {
        await _repository.AddNewStaff(staffMember);
    }
    // Return list of staff objects
    public async Task<List<StaffMember>> GetAllStaffMembers()
    {
        return await _repository.GetAllStaff();
    }
    // Return specific staff object
    public async Task<StaffMember?> RetrieveStaffById(int id)
    {
        return await _repository.FindStaffById(id);
    }
    // Return updated staff object
    public async Task UpdateStaffMemberDetails(int id, StaffMemberDto staffMember)
    {
        var existingStaffMember = await _repository.FindStaffById(id);
        if (existingStaffMember != null)
        {
            existingStaffMember.FullName = staffMember.FullName;
            existingStaffMember.Email = staffMember.Email;
            existingStaffMember.Position = staffMember.Position;
            existingStaffMember.Unit = staffMember.Unit;

            await _repository.UpdateStaff(existingStaffMember);
        }
    }
    
    public async Task DeleteStaffMember(int id)
    {
        await _repository.DeleteStaff(id);
    }
    
}