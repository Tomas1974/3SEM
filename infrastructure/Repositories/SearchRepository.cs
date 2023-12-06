using Dapper;
using infrastructure.DataModels;
using Npgsql;

namespace infrastructure.Repositories;

public class SearchRepository
{
    private NpgsqlDataSource _dataSource;

    public SearchRepository(NpgsqlDataSource dataSource)
    {
        _dataSource = dataSource; 
    }

    public IEnumerable<AvatarModel> SearchAvatar(string searchQuery)
    {
        var sql = $@"SELECT * FROM webshop.avatar WHERE avatar_name ILIKE '%{searchQuery}%' and deleted=false;";

        using (var conn = _dataSource.OpenConnection())
        {
            return conn.Query<AvatarModel>(sql, new { searchQuery });
        }
    }
}
