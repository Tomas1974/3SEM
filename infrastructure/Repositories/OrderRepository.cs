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

    public IEnumerable<Order> GetAllOrder()
    {
        var sql = @"SELECT * FROM account.order ORDER BY order_id;";

        using (var conn = _dataSource.OpenConnection())
        {
            return conn.Query<Order>(sql);
        }
    }

    public Order CreateOrder(int user_id)
    {
        var sql =
            @"INSERT INTO account.order (user_id) VALUES (@user_id) RETURNING *;";

        using (var conn = _dataSource.OpenConnection())
        {
            return conn.QueryFirst<Order>(sql, new { user_id });
        }
    }

    public void DeleteOrder(int order_id)
    {
        var sql = @"DELETE FROM account.order WHERE order_id = @order_id RETURNING *;";

        using (var conn = _dataSource.OpenConnection())
        {
            conn.QueryFirst<Order>(sql, new { order_id });
        }
    }


    public void CreateCustomerBuy(int user_id, int[] avatars)
    {
        using (var conn = _dataSource.OpenConnection())
        {

            var transaction = conn.BeginTransaction();

            var sql =
                @"INSERT INTO account.order (user_id) VALUES (@user_id) RETURNING *;";
            conn.QueryFirst<Order>(sql, new { user_id });

            var sql2 =
                @"select * from account.order where user_id= (@user_id)  and order_id = ( SELECT MAX(order_id) FROM account.order);";

            var result = conn.QueryFirst<Order>(sql2, new { user_id }, transaction);


            for (int i = 0; i < avatars.Length; i++)
            {

                var sql3 =
                    @"INSERT INTO account.customer_buy (order_id, avatar_id) VALUES (@order_id, @avatar_id) RETURNING *;";

                conn.QueryFirst(sql3, new { order_id = result.order_id, avatar_id = avatars[i] }, transaction);
            }
            transaction.Commit();
            }

        }

    public Order getLastOrderToEmail(int user_id)
    {
        var sql2 =
            @"select * from account.order where user_id= (@user_id)  and order_id = ( SELECT MAX(order_id) FROM account.order);";

         
        using (var conn = _dataSource.OpenConnection())
        {
            return   conn.QueryFirst<Order>(sql2, new { user_id });
        }
    }
       
        
    }
