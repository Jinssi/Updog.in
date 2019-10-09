using System.Threading.Tasks;

namespace Updog.Domain {
    public interface IUserService : IService<User> {
        Task<User> AdminRegisterOrUpdate(IAdminConfig config);
        Task<UserLogin?> Login(UserCredentials credentials);
        Task<UserLogin?> Register(UserRegistration registration);
        Task<User> Update(UserUpdate data, User user);
        Task<User> UpdatePassword(UserUpdatePassword data, User user);
    }
}