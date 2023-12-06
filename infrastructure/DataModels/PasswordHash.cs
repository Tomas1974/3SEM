namespace infrastructure.DataModels;

public class PasswordHash
{
    public int user_id { get; set; }
    public required string hash { get; set; }
    public required string salt { get; set; }
    
}