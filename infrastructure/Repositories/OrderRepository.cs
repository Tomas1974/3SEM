using Dapper;
using Npgsql;
using infrastructure.DataModels;

namespace infrastructure.Repositories;

public class OrderRepository
{
    private readonly NpgsqlDataSource _dataSource;

    public OrderRepository(NpgsqlDataSource dataSource)
    {
        _dataSource = dataSource;
    }

    public IEnumerable<OrderModel> GetAllOrder()
    {
        var sql = @"SELECT * FROM webshop.order ORDER BY order_id;";

        using (var conn = _dataSource.OpenConnection())
        {
            return conn.Query<OrderModel>(sql);
        }
    }

    public OrderModel CreateOrder(int user_id)
    {
        var sql =
            @"INSERT INTO webshop.order (user_id) VALUES (@user_id) RETURNING *;";

        using (var conn = _dataSource.OpenConnection())
        {
            return conn.QueryFirst<OrderModel>(sql, new { user_id });
        }
    }

    public void DeleteOrder(int order_id)
    {
        var sql = @"DELETE FROM webshop.order WHERE order_id = @order_id RETURNING *;";

        using (var conn = _dataSource.OpenConnection())
        {
            conn.QueryFirst<OrderModel>(sql, new { order_id });
        }
    }
}