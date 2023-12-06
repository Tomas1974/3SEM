using Dapper;
using Npgsql;
using infrastructure.DataModels;

namespace infrastructure.Repositories;

public class UserRepository
{
    private readonly NpgsqlDataSource _dataSource;

    public UserRepository(NpgsqlDataSource dataSource)
    {
        _dataSource = dataSource;
    }

    public User Create(RegisterCommandModel model)
    {
        const string sql = $@"
INSERT INTO account.users (full_name, street, zip, email, admin)
VALUES (@full_name, @street, @zip,@email, @admin)
RETURNING *;
";
        using (var connection = _dataSource.OpenConnection())
        {
            return connection.QueryFirst<User>(sql, new { full_name = model.full_name, street = model.Street, zip = model.Zip, email = model.Email, admin = false });
        }
    }

    public User? GetById(int id)
    {
        const string sql = $@"
SELECT * FROM account.users
WHERE user_id = @id;
";
        using (var connection = _dataSource.OpenConnection())
        {
            return connection.QueryFirstOrDefault<User>(sql, new { id });
        }
    }

    public User GetAccountInfo()
    {
        const string sql = $@"
SELECT * FROM account.users
WHERE user_id = 2;
";
        using (var connection = _dataSource.OpenConnection())
        {
            return connection.QueryFirstOrDefault<User>(sql);
        }
    }
}