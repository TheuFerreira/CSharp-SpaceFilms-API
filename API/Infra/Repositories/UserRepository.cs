using API.Domain.User.Models;
using API.Domain.User.Repositories;
using Dapper;
using System.Data;

namespace API.Infra.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IDbConnection connection;

        public UserRepository(IDbConnection connection)
        {
            this.connection = connection;
        }

        public int Create(UserModel user)
        {
            string sql = @"
INSERT INTO user (name, email, password) 
VALUES (@name, @email, @password); 
SELECT last_insert_id();
";

            object data = new
            {
                name = user.Name,
                email = user.Email,
                password = user.Password,
            };

            int lastId = connection.ExecuteScalar<int>(sql, data);
            return lastId;
        }

        public bool EmailExists(string email)
        {
            string sql = "SELECT COUNT(id_user) FROM user WHERE BINARY email = @email;";
            object data = new { email };

            int count = connection.ExecuteScalar<int>(sql, data);
            return count > 0;
        }

        public UserModel? Get(int userId)
        {
            string sql = "SELECT id_user AS UserId, name FROM user WHERE id_user = @userId;";
            object data = new { userId };

            UserModel? user = connection.Query<UserModel>(sql, data).FirstOrDefault();
            return user;
        }

        public UserModel? GetByEmailAndPassword(string email, string password)
        {
            string sql = @"
SELECT id_user AS UserId, name, email, password, email_checked AS EmailChecked 
FROM user 
WHERE BINARY email = @email 
    AND BINARY password = @password 
    AND disabled = 0;
";
            object data = new
            {
                email,
                password
            };

            UserModel? user = connection.Query<UserModel>(sql, data).FirstOrDefault();
            return user;
        }
    }
}
