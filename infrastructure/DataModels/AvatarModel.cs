using System.ComponentModel.DataAnnotations;

namespace infrastructure.DataModels;

public class AvatarModel
{
    public int AvatarId { get; set; }
    [MinLength(3)] public string AvatarName { get; set; }
    [Range(1, Int32.MaxValue)] public int AvatarPrice { get; set;  }
    public string Information { get; set;  }
    public bool Deleted { get; set;  }
    
}