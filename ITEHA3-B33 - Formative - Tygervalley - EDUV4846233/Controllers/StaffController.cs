// https://www.youtube.com/watch?v=CH2UVrkTQ8Y&t=1s
// https://zetcode.com/asp-net/modelstate/

using System.Diagnostics;
using ITEHA3_B33___Formative___Tygervalley___EDUV4846233.DTOs;
using ITEHA3_B33___Formative___Tygervalley___EDUV4846233.Models;
using ITEHA3_B33___Formative___Tygervalley___EDUV4846233.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ITEHA3_B33___Formative___Tygervalley___EDUV4846233.Controllers;

[Authorize(Roles = "Admin")]
public class StaffController : Controller
{
    private readonly StaffService _staffService;

    public StaffController(StaffService staffService)
    {
        _staffService = staffService;
    }
    // GET
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var staffMembers = await _staffService.GetAllStaffMembers();
        return View(staffMembers);
    }
    // GET Add/Edit Staff view
    // Only display form, yet if id is not null, display the data from the user being edited 
    [HttpGet]
    public async Task<IActionResult> AddEditStaff(int? id) // Id can be entered - if entered, it means edit
    {
        // id not equal to null
        if (id != null)
        {
            // retrieve staff member by id
            var staffMember = await _staffService.RetrieveStaffById(id.Value);
            // if staff member not found, return notfound
            if (staffMember == null) return NotFound();
            
            // create viewbag to pass to view bc StaffMemberDto doesn't have an Id property
            ViewBag.StaffId = staffMember.StaffId;
            var dto = new StaffMemberDto
            {
                FullName = staffMember.FullName,
                Email = staffMember.Email,
                Position = staffMember.Position,
                Unit = staffMember.Unit
            };
            return View(dto);
        }
        // 
        return View(new  StaffMemberDto());
    }

    // Add Staff
    // Receive input from form
    [HttpPost]
    
    public async Task<IActionResult> AddEditStaff(int? id, StaffMemberDto staffMemberDto)
    {
        if (!ModelState.IsValid)
        {
            return View(staffMemberDto);
        }
        // This means a member is going to be edited
        if (id != null)
        {
            await _staffService.UpdateStaffMemberDetails(id.Value, staffMemberDto);
            TempData["StatusMessage"] = "Staff member updated successfully.";
            return RedirectToAction("Index");
        }
        
        // This means a new member is going to be added
        var staffMember = new StaffMember
        {
            FullName = staffMemberDto.FullName,
            Email = staffMemberDto.Email,
            Position = staffMemberDto.Position,
            Unit = staffMemberDto.Unit
        };
        await _staffService.AddStaffMember(staffMember);
        TempData["StatusMessage"] = "Staff member added successfully.";
        return RedirectToAction("Index");
    }
    
    // Delete staff
    [HttpPost]
    
    public async Task<IActionResult> DeleteStaff(int id)
    {
        await _staffService.DeleteStaffMember(id);
        TempData["StatusMessage"] = "Staff member deleted successfully.";
        return RedirectToAction("Index");
    }
    
    // Fetch staff by ID

    [HttpPost]
    public async Task<IActionResult> Search(int id)
    { 
    if (id <= 0)
    { 
        TempData["ErrorMessage"] = "Please enter a valid staff ID.";                                                                                                                    
        return RedirectToAction("Index");                                                                                                                                               
    }
    var staffMember = await _staffService.RetrieveStaffById(id);                                                                                                                        
    if (staffMember == null)                                                                                                                                                            
    {
        TempData["ErrorMessage"] = "No staff member found with that ID."; 
        return RedirectToAction("Index"); 
    }
    TempData["StatusMessage"] = "Staff member loaded successfully.";
    return RedirectToAction("AddEditStaff", new { id });
    }
    
}