namespace AdsPlatformsAPI.Exceptions
{
    public class InvalidLineFormatException(string line, string msg) 
        : BadRequestException($"Ошибка в строке: {line}. {msg}");
}
