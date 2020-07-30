using System;
using System.Collections.Generic;
using System.Text;

namespace DisplayConfigLib
{
    public enum ErrorCode
    {
        OK,
        EMPTY,
        VALUE,
        TIMEOUT
    }

    public class DisplayException : RaGae.ExceptionLib.BaseException<ErrorCode>
    {
        public DisplayException(ErrorCode errorCode, string errorParameter) : base(errorCode, errorParameter) { }

        public override string ErrorMessage()
        {
            switch (ErrorCode)
            {
                case ErrorCode.OK:
                    return "TILT: Should not be reached";
                case ErrorCode.EMPTY:
                    return $"Empty Value: {base.Message}";
                case ErrorCode.VALUE:
                    return $"Incorrect Value: {base.Message}";
                case ErrorCode.TIMEOUT:
                    return $"Timeout: {base.Message}";
                default:
                    return string.Empty;
            }
        }
    }
}
