using MyWebApiApp.dataAccess;
using WebAPI.entities;

namespace WebAPI.repositories.impl
{
    public class RoleRepositoryImpl : IRoleRepository
    {
        private readonly MyDbContext _context;

        public RoleRepositoryImpl(MyDbContext context)
        {
            _context = context;
        }

        public string GetRoleName(int id)
        {
            Role role = _context.Roles.SingleOrDefault(r => r.Id == id);
            return role.Name;
        }
    }
}
