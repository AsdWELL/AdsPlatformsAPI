namespace AdsPlatformsAPI.Exceptions
{
    public class InvalidLineFormatException(string line, string msg) 
        : Exception($"Ошибка в строке: {line}. {msg}");
}
