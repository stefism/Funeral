namespace Funeral.App.ViewModels
{
    public class UserInfoViewModel
    {
        public string Id { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }

        public bool IsEmailConfirmed { get; set; }

        public int ObituaryCount { get; set; }

        public int PicturesCount { get; set; }
    }
}
