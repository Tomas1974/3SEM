using System.ComponentModel.DataAnnotations;

namespace infrastructure.DataModels;

public class RegisterCommandModel
{
    
    [Required] [MinLength(2)] public required string full_name { get; set; }
    [Required] [MinLength(2)] public required string Street { get; set; }
    [Required] [Range( 1000, 9999)]public required int Zip { get; set; }
    [Required] [MinLength(9)] public required string Email { get; set; }
    [Required] [MinLength(8)] public required string password { get; set; }
    
}