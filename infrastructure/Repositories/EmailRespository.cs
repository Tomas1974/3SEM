using Dapper;
using infrastructure.DataModels;
using Npgsql;

namespace infrastructure.Repositories;

public class EmailRepository
{
    private readonly NpgsqlDataSource _dataSource;

    public EmailRepository(NpgsqlDataSource dataSource)
    {
        _dataSource = dataSource;
    }

    public UserModel GetOrdersUser(int order_id)
    {
        var sql = @"SELECT * 
        FROM webshop.users
        INNER JOIN webshop.order on webshop.order.user_id=webshop.users.user_id 
        where webshop.order.order_id=@order_id";

        using (var conn = _dataSource.OpenConnection())
        {
            return conn.QueryFirst<UserModel>(sql, new { order_id });
        }
    }

    public IEnumerable<AvatarModel> GetOrdersAvatars(int order_id)
    {
        var sql = @" SELECT * 
        FROM webshop.avatar
        INNER JOIN webshop.customer_buy on webshop.customer_buy.avatar_id=webshop.avatar.avatar_id 
         where webshop.customer_buy.order_id=@order_id";

        using (var conn = _dataSource.OpenConnection())
        {
            return conn.Query<AvatarModel>(sql, new { order_id });
        }
    }
}