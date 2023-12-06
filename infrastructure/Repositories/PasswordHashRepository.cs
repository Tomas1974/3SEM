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
    
    public void Create(int id, string hash, string salt)
    {
        const string sql = $@"INSERT INTO webshop.password_hash (user_id, hash, salt) VALUES (@id, @hash, @salt)";
        using var connection = _dataSource.OpenConnection();
        connection.Execute(sql, new {id, hash, salt  });
    }
    
    public PasswordHashModel GetByEmail(string mail)
    {
        const string sql = $@"

    SELECT * FROM account.password_hash
    JOIN account.users ON account.password_hash.user_id = account.users.user_id
    WHERE email = @email;

    SELECT 
    password_hash.user_id as {nameof(PasswordHashModel.Id)},
    hash as {nameof(PasswordHashModel.Hash)},
    salt as {nameof(PasswordHashModel.Salt)}
    FROM webshop.password_hash
    JOIN webshop.users ON webshop.password_hash.user_id = webshop.users.user_id
    WHERE email = @mail;

    ";
        using var connection = _dataSource.OpenConnection();
        return connection.QuerySingle<PasswordHashModel>(sql, new { mail });
    }
}

