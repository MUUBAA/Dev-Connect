using dev_connect.Server.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using static dev_connect.Server.Data.Exceptions.DataExceptions;
using static dev_connect.Server.Data.Exceptions.DominExceptions;

namespace dev_connect.Server.Data.Filters
{
    public class CustomException : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if(context.Exception is NotFoundException)
            {
                var notFoundException = context.Exception as NotFoundException;
                var response = new GenericApiResponse<Exception>(false, notFoundException.Message);

                context.Result = new ObjectResult(response)
                {
                    StatusCode = 404,
                };
            }
            else if(context.Exception is InvalidAuthException)
            {
                var invalidAuthException = context.Exception as InvalidAuthException;
                var response = new GenericApiResponse<Exception>(false, invalidAuthException.Message);

                context.Result = new ObjectResult(response)
                {
                    StatusCode = 401,
                };
            }
            else if (context.Exception is UserInActivateException)
            {
                var userInActiveException = context.Exception as UserInActivateException;
                var response = new GenericApiResponse<Exception>(false, userInActiveException.Message);

                context.Result = new ObjectResult(response)
                {
                    StatusCode = 401,
                };
            }
            else
            {
                var response = new GenericApiResponse<Exception>(false, context.Exception.Message);
                context.Result = new ObjectResult(response)
                {
                    StatusCode = 400
                };
            }
            context.ExceptionHandled = true;
        }
    }
}
