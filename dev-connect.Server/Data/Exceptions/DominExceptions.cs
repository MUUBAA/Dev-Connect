namespace dev_connect.Server.Data.Exceptions
{
    public class DominExceptions
    {
        public class InvalidAuthException(string message) : Exception(message) {}
        public class UserDeletedException(string message) : Exception(message) {}
        public class UserInActivateException(string message) : Exception(message) {}
        public class UserProfilingException(string message) : Exception(message) {}
    }
}
