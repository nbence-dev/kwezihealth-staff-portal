// https://www.youtube.com/watch?v=BlavB2Z4MIw
// https://medium.com/@MJQuinn/asp-net-feedback-through-tempdata-91ef08827a90
using System.ComponentModel.DataAnnotations;

namespace ITEHA3_B33___Formative___Tygervalley___EDUV4846233.DTOs;

// DTO is basically the data you receive from a client e.g. submission form
public class StaffMemberDto
{ 

    
    [Required(ErrorMessage = "Full name is required")]
    public string FullName { get; set; } = string.Empty;
    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Email is invalid")]
    public string Email { get; set; }  = string.Empty;
    [Required(ErrorMessage = "Position is required")]
    public string Position { get; set; } = string.Empty;
    
    [Range(1, int.MaxValue, ErrorMessage = "Unit must be a positive number")]
    public int Unit { get; set; }
}