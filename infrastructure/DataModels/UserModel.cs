
namespace infrastructure.DataModels;

public class UserModel
{
    public int Id { get; set; }
    
    public required string Name { get; set; }
    
    public required string Mail { get; set; }
    
    public string? Admin { get; set; }
}