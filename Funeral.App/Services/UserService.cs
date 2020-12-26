using Funeral.App.ViewModels;
using Funeral.Data;
using System.Collections.Generic;
using System.Linq;

namespace Funeral.App.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext db;

        public UserService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public ICollection<UserInfoViewModel> ShowUsersInfo()
        {
            return db.Users.Select(u => new UserInfoViewModel
            {
                Id = u.Id,
                Username = u.UserName,
                Email = u.Email,
                ObituaryCount = db.UserObituaries.Where(uo => uo.UserId == u.Id).Count(),
                PicturesCount = db.Pictures.Where(p => p.UserId == u.Id).Count(),
            }).ToList();
        }
    }
}
