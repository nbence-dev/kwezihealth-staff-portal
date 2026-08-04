// https://www.youtube.com/watch?v=CH2UVrkTQ8Y&t=1s

using System.Diagnostics;
using ITEHA3_B33___Formative___Tygervalley___EDUV4846233.DTOs;
using ITEHA3_B33___Formative___Tygervalley___EDUV4846233.Models;
using ITEHA3_B33___Formative___Tygervalley___EDUV4846233.Services;
using Microsoft.AspNetCore.Mvc;

namespace ITEHA3_B33___Formative___Tygervalley___EDUV4846233.Controllers;

public class StaffController : Controller
{
    private readonly StaffService _staffService;

    public StaffController(StaffService staffService)
    {
        _staffService = staffService;
    }
    // GET
    public async Task<IActionResult> Index()
    {
        var staffMembers = await _staffService.GetAllStaffMembers();
        return View(staffMembers);
    }
    // GET Add/Edit Staff view
    // Only display form
    public async Task<IActionResult> AddEditStaff(int? id) // Id can be entered - if entered, it means edit
    {
        if (id != null)
        {
            var staffMember = await _staffService.RetrieveStaffById(id.Value);
            return View(staffMember);
        }
        return View();
    }

    // Add Staff
    // Receive input from form
    [HttpPost]
    public async Task<IActionResult> AddEditStaff(int? id, StaffMemberDto staffMemberDto)
    {
        if (id != null)
        {
            await _staffService.UpdateStaffMemberDetails(id.Value, staffMemberDto);
            return RedirectToAction("Index");
        }
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var staffMember = new StaffMember
        {
            FullName = staffMemberDto.FullName,
            Email = staffMemberDto.Email,
            Position = staffMemberDto.Position,
            Unit = staffMemberDto.Unit
        };
        await _staffService.AddStaffMember(staffMember);
        
        return RedirectToAction("Index");
    }
    
    // Delete staff
    [HttpPost]
    public async Task<IActionResult> DeleteStaff(int id)
    {
        await _staffService.DeleteStaffMember(id);
        return RedirectToAction("Index");
    }
    
    // Fetch staff by ID
}