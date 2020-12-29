using Funeral.App.ViewModels;
using System.Collections.Generic;

namespace Funeral.App.Services
{
    public interface IUserService
    {
        ICollection<UserInfoViewModel> ShowUsersInfo();

        string GetUserNameById(string id);
    }
}
