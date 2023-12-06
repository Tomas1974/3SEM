using Dapper;
using Npgsql;
using infrastructure.DataModels;

namespace infrastructure.Repositories;

public class PasswordHashRepository
{
    private readonly NpgsqlDataSource _dataSource;

    public PasswordHashRepository(NpgsqlDataSource dataSource)
    {
        _dataSource = dataSource;
    }

    public PasswordHash GetByEmail(string email)
    {
        const string sql = $@"
    SELECT * FROM account.password_hash
    JOIN account.users ON account.password_hash.user_id = account.users.user_id
    WHERE email = @email;
    ";
        using var connection = _dataSource.OpenConnection();
        return connection.QuerySingle<PasswordHash>(sql, new { email });
    }
    
    public void Create(int userId, string hash, string salt)
    {
        const string sql = $@"INSERT INTO account.password_hash (user_id, hash, salt) VALUES (@userId, @hash, @salt)";
        using var connection = _dataSource.OpenConnection();
        connection.Execute(sql, new { userId, hash, salt  });
    }
}

