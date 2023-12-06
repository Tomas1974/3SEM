namespace infrastructure.DataModels;

public class PasswordHashModel
{

    public int user_id { get; set; }
    public required string hash { get; set; }
    public required string salt { get; set; }
    

    public int Id { get; set; }
    
    public required string Hash { get; set; }
    
    public required string Salt { get; set; }

}