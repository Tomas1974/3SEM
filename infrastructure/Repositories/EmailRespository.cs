using Dapper;
using infrastructure.DataModels;
using Npgsql;

namespace infrastructure.Repositories;

public class EmailRespository
{
    private readonly NpgsqlDataSource _dataSource;

    public EmailRespository(NpgsqlDataSource dataSource)
    {
        _dataSource = dataSource;
    }

    public User GetOrdersUser(int order_id)
    {
        var sql = @"SELECT * 
        FROM account.users
        INNER JOIN account.order on account.order.user_id=account.users.user_id 
        where account.order.order_id=@order_id";

        using (var conn = _dataSource.OpenConnection())
        {
            return conn.QueryFirst<User>(sql, new { order_id });
        }
    }
    
    public IEnumerable<Avatar> GetOrdersAvatars(int order_id)
    {
        var sql = @" SELECT * 
        FROM account.avatar
        INNER JOIN account.customer_buy on account.customer_buy.avatar_id=account.avatar.avatar_id 
         where account.customer_buy.order_id=@order_id";
        
        using (var conn = _dataSource.OpenConnection())
        {
            return conn.Query<Avatar>(sql, new { order_id });
        }
    }
}