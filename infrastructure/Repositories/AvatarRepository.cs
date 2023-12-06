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
    
    
    public IEnumerable<AvatarModel> GetAllAvatars()
    {
        var sql = @"SELECT * FROM webshop.avatar where deleted=false ORDER BY avatar_id;";

        using (var conn = _dataSource.OpenConnection())
        {
            return conn.Query<AvatarModel>(sql);
        }
    }
    
    
    public AvatarModel CreateAvatar(AvatarModel avatar)
    {
        var sql =
            @" INSERT INTO webshop.avatar (avatar_name, avatar_price, information,deleted) VALUES (@avatar_name, @avatar_price, @information, false) RETURNING *;";

        using (var conn = _dataSource.OpenConnection())
        {
            return conn.QueryFirst<AvatarModel>(sql, new { avatar_name=avatar.AvatarName, avatar_price=avatar.AvatarPrice, information=avatar.Information});
        }
    }
    
    public AvatarModel UpdateAvatar(int avatar_id, AvatarModel avatar)
    {
        var sql =
            @"UPDATE webshop.avatar SET avatar_name = @avatar_name, avatar_price = @avatar_price, information = @information where avatar_id = @avatar_id
RETURNING *";

        using (var conn = _dataSource.OpenConnection())
        {
            return conn.QueryFirst<AvatarModel>(sql, new { avatar_id, avatar_name=avatar.AvatarName, avatar_price=avatar.AvatarPrice,information=avatar.Information});
        }
    }

    
    public void DeleteAvatar(int avatar_id)
    {
        
        var sql =
            @"UPDATE webshop.avatar SET deleted = true where avatar_id = @avatar_id
RETURNING *"; 
       
        using (var conn = _dataSource.OpenConnection())
        {
            conn.QueryFirst<AvatarModel>(sql, new { avatar_id });
        }
    }
    
    public AvatarModel CheckIfNameExist(string avatar_name)    {

        var sql = $@"SELECT * FROM webshop.avatar WHERE avatar_name = @avatar_name;";

        using (var conn = _dataSource.OpenConnection())
        {
            return conn.QueryFirstOrDefault<AvatarModel>(sql, new { avatar_name });
        }
    }
    public string GetAvatarInformation(int avatar_id)
    {
        var sql = @"SELECT information FROM webshop.avatar where deleted = false AND avatar_id = @avatar_id;";

        using (var conn = _dataSource.OpenConnection())
        {
            return conn.QueryFirst<string>(sql, new { avatar_id });
        }
    }
}