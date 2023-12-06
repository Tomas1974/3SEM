
namespace infrastructure.DataModels;

public class User
{
    public int user_id { get; set; }
    public required string full_name { get; set; }
    public required string Street { get; set; }
    public required int Zip { get; set; }
    public required string Email { get; set; }
    public required bool admin { get; set; }
}