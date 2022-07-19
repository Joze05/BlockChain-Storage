using API1.Models;
namespace API1.Interfaces
{
    public interface IUser
    {
        public List<User> getUsers();
        public string deleteUser(string id);
        public List<User> postUser(User user);
        public User validateUser(User user);
    }
}
