using SmartFactoryWebApi.Dtos;
using SmartFactoryWebApi.Models;

namespace SmartFactoryWebApi.InMemoryRepositories
{
    public static class UserRepository
    {
        public static List<User> Users { get; private set; } = new List<User>()
        {
            new User { Id = 1, FirstName = "John", LastName = "Doe", Email = "john@gmail.com", Password = "password",Role=UserRoles.SuperUser },
            new User { Id = 2, FirstName = "Jane", LastName = "Smith", Email = "jane@gmail.com", Password = "password",Role=UserRoles.Maintenance },
            new User { Id = 3, FirstName = "Alice", LastName = "Johnson", Email = "alice@gmail.com", Password = "password",Role=UserRoles.FactoryManager },
            new User { Id = 4, FirstName = "Bob", LastName = "Brown", Email = "bob@gmail.com", Password = "password",Role=UserRoles.Administrator },
            new User { Id = 5, FirstName = "Charlie", LastName = "Davis", Email = "charlie@gmail.com", Password = "password",Role=UserRoles.Supervisors }
        };

        public static User? GetUserById(int id)
        {
            var user=Users.FirstOrDefault(u => u.Id == id);

            return user;
        }

        public static User? GetUserByEmail(string email)
        {
            var user = Users.FirstOrDefault(u => u.Email == email);

            return user;
        }

        public static List<User> GetAllUsers()
        {
            return Users;
        }

    }
}
