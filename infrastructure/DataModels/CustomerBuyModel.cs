using System.ComponentModel.DataAnnotations;

namespace infrastructure.DataModels;

public class CustomerBuyModel
{
    public int CustomerBuyId { get; set; }
    public int OrderId { get; set; }
    public int AvatarId { get; set; }
    
    
    
}