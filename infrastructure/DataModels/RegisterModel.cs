using System.ComponentModel.DataAnnotations;

namespace infrastructure.DataModels;

public class RegisterModel
{
    
    [Required] [MinLength(2)] public required string Name { get; set; }
   
    [Required] [MinLength(9)] public required string Mail { get; set; }
   
    [Required] [MinLength(8)] public required string Password { get; set; }
   
    [Required]  public required string Admin { get; set; }

    
}