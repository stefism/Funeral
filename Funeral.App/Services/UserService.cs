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
                IsEmailConfirmed = u.EmailConfirmed,
                ObituaryCount = db.UserObituaries.Where(uo => uo.UserId == u.Id).Count(),
                PicturesCount = db.Pictures.Where(p => p.UserId == u.Id).Count(),                
            }).OrderBy(u => u.Username).ToList();
        }

        public string GetUserNameById(string id)
        {
            return db.Users.Where(u => u.Id == id).Select(u => u.UserName).FirstOrDefault();
        }

        public CountsViewModel ShowUsersAndPicturesCount()
        {
            var usersCount = db.Users.Where(u => u.EmailConfirmed).Count();
            var obituariesCount = db.Obituaries.Count();
            var picturesCount = db.Pictures.Count();

            var counts = new CountsViewModel
            {
                TotalUsersCount = usersCount,
                TotalObituaryCount = obituariesCount,
                TotalUserPicturesCount = picturesCount,
            };

            return counts;
        }
    }
}
