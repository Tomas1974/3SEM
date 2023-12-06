using Dapper;
using infrastructure.DataModels;
using Npgsql;

namespace infrastructure.Repositories;

public class AvatarRepository
{
    private readonly NpgsqlDataSource _dataSource;

    public AvatarRepository(NpgsqlDataSource dataSource)
    {
        _dataSource = dataSource;
    }
    
    
    public IEnumerable<Avatar> GetAllAvatars()
    {
        var sql = @"SELECT * FROM account.avatar where deleted=false ORDER BY avatar_id;";

        using (var conn = _dataSource.OpenConnection())
        {
            return conn.Query<Avatar>(sql);
        }
    }
    
    
    public Avatar CreateAvatar(Avatar avatar)
    {
        var sql =
            @" INSERT INTO account.avatar (avatar_name, avatar_price, information,deleted) VALUES (@avatar_name, @avatar_price, @information, false) RETURNING *;";

        using (var conn = _dataSource.OpenConnection())
        {
            return conn.QueryFirst<Avatar>(sql, new { avatar_name=avatar.avatar_name, avatar_price=avatar.avatar_price, information=avatar.information });
        }
    }
    
    public Avatar UpdateAvatar(int avatar_id, Avatar avatar)
    {
        var sql =
            @"UPDATE account.avatar SET avatar_name = @avatar_name, avatar_price = @avatar_price, information = @information where avatar_id = @avatar_id
RETURNING *";

        using (var conn = _dataSource.OpenConnection())
        {
            return conn.QueryFirst<Avatar>(sql, new { avatar_id, avatar_name=avatar.avatar_name, avatar_price=avatar.avatar_price,information=avatar.information});
        }
    }

    
    public void DeleteAvatar(int avatar_id)
    {
        
        var sql =
            @"UPDATE account.avatar SET deleted = true where avatar_id = @avatar_id
RETURNING *"; 
       
        using (var conn = _dataSource.OpenConnection())
        {
            conn.QueryFirst<Avatar>(sql, new { avatar_id });
        }
    }
    
    public Avatar CheckIfNameExist(string avatar_name)    {

        var sql = $@"SELECT * FROM account.avatar WHERE avatar_name = @avatar_name;";

        using (var conn = _dataSource.OpenConnection())
        {
            return conn.QueryFirstOrDefault<Avatar>(sql, new { avatar_name });
        }
    }
    public string GetAvatarInformation(int avatar_id)
    {
        var sql = @"SELECT information FROM account.avatar where deleted = false AND avatar_id = @avatar_id;";

        using (var conn = _dataSource.OpenConnection())
        {
            return conn.QueryFirst<string>(sql, new { avatar_id });
        }
    }
}