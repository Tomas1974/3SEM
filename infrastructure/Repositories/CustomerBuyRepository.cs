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

    public IEnumerable<CustomerBuyModel> GetAllCustomerBuy()
    {
        var sql =
            @"SELECT * FROM webshop.customer_buy ORDER BY order_id;";
        using (var conn = _dataSource.OpenConnection())
        {
            return conn.Query<CustomerBuyModel>(sql);
        }
    }

    public CustomerBuyModel CreateCustomerBuy(CustomerBuyModel customerBuy)
    {
        var sql =
            @"INSERT INTO webshop.customer_buy (order_id, avatar_id) VALUES (@order_id, @avatar_id) RETURNING *;";

        using (var conn = _dataSource.OpenConnection())
        {
            return conn.QueryFirst<CustomerBuyModel>(sql, new { order_id=customerBuy.OrderId, avatar_id=customerBuy.AvatarId });
        }
    }
    
    public void DeleteCustomerBuy(int customer_buy_id)
    {
        var sql = @"DELETE FROM webshop.customer_buy WHERE webshop.customer_buy.customer_buy_id = @customer_buy_id RETURNING *;";

        using (var conn = _dataSource.OpenConnection())
        {
            conn.QueryFirst<CustomerBuyModel>(sql, new {customer_buy_id});
        }
    }
}