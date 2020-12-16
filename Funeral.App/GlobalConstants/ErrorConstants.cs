namespace Funeral.App.GlobalConstants
{
    public static class ErrorConstants
    {
        public const string FileTypeError = "Непозволено разширение на файл. Можете да качвате само файлове с разширения .JPG, .PNG и .GIF";

        public const string FileMaxSizeError = "Файла е твърде голям. Максималната големина на файла не трябва да надвишава 1 мега байт.";

        public const string ImageIsNotImage = "Изглежда файла, който се опитвате да качите не съдържа изображение. Ако сте сменили разширението на файла но в него няма картинка, няма да успеете да го качите.";

        public const string UsernameMustBe30Characters = "Потребителското име трябва да бъде с максимална дължина 30 символа.";

        public const string PasswordMustBeBetween6And100Characters = "Паролата трябва да бъде с дължжина между {2} и {1} символа.";

        public const string PasswordIsNotMatch = "Двете въведени пароли не си съвпадат.";

        public const string InvalidEmail = "И-мейл адреса, който въвеждате е невалиден. Моля, въведете валиден И-мейл адрес.";

        public const string RequiredField = "Задължително трябва да попълните това поле.";

        public const string MaxUserPictureError = "Надвишавате максималното количество от 5 снимки, които можете да качите. Моля, изтрийте снимка от архива си, за да можете да качите нова.";

        public const string NoSelectedFileError = "Нямате избран файл за качване. Моля, първо изберете файл за качване и опитайте отново.";
    }
}
