using ShoppingCart.Helpers;
using ShoppingCart.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShoppingCart.Interfaces
{
    public interface IUserData
    {

        Task AddUser(User user);
        Task<IEnumerable<User>> GetUsers();

        Task<LoginRequest> LogInUser(string username,string password);

        Task UpdateUser(User user);

        Task DeleteUser(int UserId);
    }
}
