// https://stackoverflow.com/questions/3406550/unique-constraint-with-data-annotation
// https://www.codemag.com/Article/2301031/The-Rich-Set-of-Data-Annotation-and-Validation-Attributes-in-.NET
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace ITEHA3_B33___Formative___Tygervalley___EDUV4846233.Models;

[Index(nameof(Username), IsUnique = true)]
public class SystemAdmin
{
    [Required]
    [Key]
    public string Username { get; set; }
    [Required]
    [MinLength(6, ErrorMessage = "{0} must be at least {1} characters long.")]
    public string Password { get; set; }
}