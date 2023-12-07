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
    
    public UserModel Create(RegisterModel model)
    {
        const string sql = $@"
INSERT INTO webshop.users (full_name, email, admin)
VALUES (@name, @mail, @admin)
RETURNING
    user_id as {nameof(UserModel.Id)},
    full_name as {nameof(UserModel.Name)},
    email as {nameof(UserModel.Mail)},
    admin as {nameof(UserModel.Admin)}
    ;
";
        using (var connection = _dataSource.OpenConnection())
        {
            return connection.QueryFirst<UserModel>(sql, new { name = model.Name,  mail = model.Mail, admin = model.Admin });
        }
    }
    
    public UserModel? GetById(int id)
    {
        const string sql = $@"
SELECT
   user_id as {nameof(UserModel.Id)},
    full_name as {nameof(UserModel.Name)},
    email as {nameof(UserModel.Mail)},
    admin as {nameof(UserModel.Admin)}
FROM webshop.users
WHERE user_id = @id;
";
        using (var connection = _dataSource.OpenConnection())
        {
            return connection.QueryFirstOrDefault<UserModel>(sql, new { id });
        }
    }
}