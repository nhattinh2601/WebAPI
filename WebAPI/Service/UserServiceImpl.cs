using authentication_authorization_webapi.DataAccess;
using authentication_authorization_webapi.DTO;
using authentication_authorization_webapi.Entity;

namespace authentication_authorization_webapi.Service
{
    public class UserServiceImpl : IUserService
    {
        private readonly MyDbContext _context;

        public UserServiceImpl(MyDbContext context)
        {
            _context = context;
        }

        public List<User> getAll()
        {
            var users = _context.Users.Select(u => new User
            {
                Id = u.Id,
                fullname = u.fullname,
                email = u.email,
                username = u.username,
                password = u.password,
                role_id = u.role_id
            });
            return users.ToList();
        }

    }
}
