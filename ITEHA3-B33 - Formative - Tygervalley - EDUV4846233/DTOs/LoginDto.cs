// Deliverable 3: Controllers, Authentication & Views — validated input model for the AccessController Login action.
using System.ComponentModel.DataAnnotations;

namespace ITEHA3_B33___Formative___Tygervalley___EDUV4846233.DTOs;

public class LoginDto
{
    [Required(ErrorMessage = "Username is required")]
    public string Username { get; set; } = string.Empty;
    [Required(ErrorMessage = "Password is required")]
    public string Password { get; set; } = string.Empty;
}