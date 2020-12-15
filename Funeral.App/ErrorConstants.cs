namespace Funeral.App
{
    public static class ErrorConstants
    {
        public const string FileTypeError = "Непозволено разширение на файл. Можете да качвате само файлове с разширения .JPG, .PNG и .GIF";

        public const string FileMaxSizeError = "Файла е твърде голям. Максималната големина на файла не трябва да надвишава 1 мега байт.";

        public const string ImageIsNotImage = "Изглежда файла, който се опитвате да качите не съдържа изображение. Ако сте сменили разширението на файла но в него няма картинка, няма да успеете да го качите.";
    }
}
