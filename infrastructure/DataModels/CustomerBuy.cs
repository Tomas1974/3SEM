using System.ComponentModel.DataAnnotations;

namespace infrastructure.DataModels;

public class CustomerBuy
{
    public int customer_buy_id { get; set; }
    public int order_id { get; set; }
    public int avatar_id { get; set; }
    
    
    
}