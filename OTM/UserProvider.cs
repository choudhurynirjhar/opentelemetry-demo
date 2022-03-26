using Dapper;
using System.Data.SqlClient;

namespace OTM;

public interface IUserProvider
{
    User[] Get();
}

public class UserProvider : IUserProvider
{
    private readonly string connectionString;

    public UserProvider(string connectionString)
    {
        this.connectionString = connectionString;
    }

    public User[] Get()
    {
        using var connection = new SqlConnection(connectionString);
        return connection.Query<User>
            ("SELECT [Id],[Name],[Age] FROM [Demo].[dbo].[Employee]").ToArray();
    }
}

public class User
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Age { get; set; }
}