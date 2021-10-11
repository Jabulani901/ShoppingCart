using Dapper;
using Microsoft.Data.SqlClient;
using ShoppingCart.Helpers;
using ShoppingCart.Interfaces;
using ShoppingCart.Models;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace ShoppingCart.Services
{
    public class UserDataService : IUserData
    {
        private readonly string connectionString = "Server=;Database=Login;Trusted_Connection=True;";

        public async Task AddUser(User user)
        {
            using (var sqlConnection = new SqlConnection(connectionString))
            {
                await sqlConnection.OpenAsync();
                var dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("@UserName", user.UserName);
                dynamicParameters.Add("@Email", user.Email);
                dynamicParameters.Add("@Password", user.Password);

                await sqlConnection.ExecuteAsync(
                    "spAddUser",
                    dynamicParameters,
                    commandType: CommandType.StoredProcedure);
            }
        }
        public async Task DeleteUser(int UserId)
        {
            using (var sqlConnection = new SqlConnection(connectionString))
            {
                await sqlConnection.OpenAsync();
                var dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("@UserId", UserId);
                await sqlConnection.ExecuteAsync(
                    "spDeleteUser",
                    dynamicParameters,
                    commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<LoginRequest> LogInUser(string username, string password)
        {
            using (var sqlConnection = new SqlConnection(connectionString))
            {
                await sqlConnection.OpenAsync();
                var dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("@Username", username);
                dynamicParameters.Add("@Password", password);
                return await sqlConnection.QuerySingleOrDefaultAsync<LoginRequest>(
                    "spLogInUser",
                    dynamicParameters,
                    commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            using (var sqlConnection = new SqlConnection(connectionString))
            {
                await sqlConnection.OpenAsync();
                return await sqlConnection.QueryAsync<User>(
                    "spGetUsers",
                    null,
                    commandType: CommandType.StoredProcedure);
            }
        }

        public async Task UpdateUser(User user)
        {
            using (var sqlConnection = new SqlConnection(connectionString))
            {
                await sqlConnection.OpenAsync();
                var dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("@UserName", user.UserName);
                dynamicParameters.Add("@Email", user.Email);
                dynamicParameters.Add("@Password", user.Password);
                await sqlConnection.ExecuteAsync(
                    "spUpdateUser",
                    dynamicParameters,
                    commandType: CommandType.StoredProcedure);
            }
        }
    }
}
