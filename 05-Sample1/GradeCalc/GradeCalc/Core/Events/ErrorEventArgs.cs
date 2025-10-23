using System;

namespace GradeCalc.Core.Events
{
    /// <summary>
    ///     EventArgs which can be used to supply an error message
    /// </summary>
    public sealed class ErrorEventArgs : EventArgs
    {
        /// <summary>
        ///     default ctor
        /// </summary>
        /// <param name="errMsg">A message describing the error</param>
        public ErrorEventArgs(string errMsg)
        {
            if (string.IsNullOrWhiteSpace(errMsg))
            {
                errMsg = "unknown error";
            }
            ErrMsg = errMsg;
        }

        /// <summary>
        ///     Gets the error message
        /// </summary>
        public string ErrMsg { get; }
    }
}