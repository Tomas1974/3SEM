using Dapper;
using Npgsql;
using infrastructure.DataModels;

namespace infrastructure.Repositories;

public class CustomerBuyRepository
{
    private readonly NpgsqlDataSource _dataSource;

    public CustomerBuyRepository(NpgsqlDataSource dataSource)
    {
        _dataSource = dataSource;
    }

    public IEnumerable<CustomerBuy> GetAllCustomerBuy()
    {
        var sql =
            @"SELECT * FROM account.customer_buy ORDER BY order_id;";
        using (var conn = _dataSource.OpenConnection())
        {
            return conn.Query<CustomerBuy>(sql);
        }
    }

    public CustomerBuy CreateCustomerBuy(CustomerBuy customerBuy)
    {
        var sql =
            @"INSERT INTO account.customer_buy (order_id, avatar_id) VALUES (@order_id, @avatar_id) RETURNING *;";

        using (var conn = _dataSource.OpenConnection())
        {
            return conn.QueryFirst<CustomerBuy>(sql, new { order_id=customerBuy.order_id, avatar_id=customerBuy.avatar_id });
        }
    }
    
    public void DeleteCustomerBuy(int customer_buy_id)
    {
        var sql = @"DELETE FROM account.customer_buy WHERE account.customer_buy.customer_buy_id = @customer_buy_id RETURNING *;";

        using (var conn = _dataSource.OpenConnection())
        {
            conn.QueryFirst<CustomerBuy>(sql, new {customer_buy_id});
        }
    }
}