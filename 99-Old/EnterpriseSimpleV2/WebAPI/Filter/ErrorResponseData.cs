using System;
using System.Text.RegularExpressions;

namespace EnterpriseSimpleV2.WebAPI.Filter
{
    public sealed class ErrorResponseData
    {
        public ErrorResponseData(string error, object message)
        {
            Error = error;
            Message = message;
        }

        public ErrorResponseData(Exception exception)
        {
            Error = Regex.Replace(exception.GetType().Name, "(Error|Exception)$", string.Empty);
            Message = exception.Message;
        }

        public string Error { get; set; }

        public object Message { get; set; }
    }
}