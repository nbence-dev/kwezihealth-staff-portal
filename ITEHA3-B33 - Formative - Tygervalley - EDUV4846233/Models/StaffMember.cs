// https://learn.microsoft.com/en-us/aspnet/web-api/overview/older-versions/using-web-api-1-with-entity-framework-5/using-web-api-with-entity-framework-part-2
// https://archidevineer.com/post/entity-framework-common-convention-error/


using System.ComponentModel.DataAnnotations;

namespace ITEHA3_B33___Formative___Tygervalley___EDUV4846233.Models;

public class StaffMember
{
    [Key]
    public int StaffId { get; set; }
    [Required]
    
    public string FullName { get; set; }
    [Required]
    
    public string Email { get; set; }
    // Position - The specific job role aka Doctor, Nurse etc.
    [Required]
    
    public string Position { get; set; }
    // Unit - typically refers to the floor or like "room" or department.
    // Could argue that a unit could be something like A12 which means it could contain chars, so use a string or int?
    [Required]
    
    public int Unit { get; set; }
       
}