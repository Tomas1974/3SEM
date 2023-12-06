using infrastructure.DataModels;

namespace service;

public class SessionData
{
    public required int User_Id { get; init; }
    public required bool admin { get; init; }

    public static SessionData FromUser(User user)
    {
        return new SessionData { User_Id = user.user_id, admin = user.admin };
    }

    public static SessionData FromDictionary(Dictionary<string, object> dict)
    {
        return new SessionData { User_Id = (int)dict[Keys.User_Id], admin = (bool)dict[Keys.admin] };
    }

    public Dictionary<string, object> ToDictionary()
    {
        return new Dictionary<string, object> { { Keys.User_Id, User_Id }, { Keys.admin, admin } };
    }

    public static class Keys
    {
        public const string User_Id = "u";
        public const string admin = "a";
    }
}