using System;

namespace AngularJSAuthentication.API.Exceptions
{
    public class FailedResetPasswordException : Exception
    {
        public FailedResetPasswordException() : this("Failed to reset password")
        {

        }

        public FailedResetPasswordException(string message)
            : base(message)
        {
        }
    }
}