using Funeral.Data;
using System.Linq;

namespace Funeral.App.Services
{
    public class UserPictureService : IUserPictureService
    {
        private readonly ApplicationDbContext db;

        public UserPictureService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public int GetUserPictureCount(string userId)
        {
            return db.Pictures.Where(p => p.UserId == userId).Count();
        }
    }
}
