namespace dev_connect.Server.Data.Exceptions
{
    public class DataExceptions
    {
        public class NotFoundException(string message) : Exception(message) {}
        public class EntityDuplicateException(string message): Exception(message) {}
    }
}
