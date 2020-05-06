using System;

namespace ProjectLambo.Bitmex
{
    [Serializable]
    public class BitmexNoResponse : Exception
    {
        public BitmexNoResponse()
            : base("Request returned empty") { }

        public BitmexNoResponse(string message)
            : base(message) { }
    }

    [Serializable]
    public class BitmexResponseParseError : Exception
    {
        public BitmexResponseParseError()
            : base("Response couldn't be parsed as expected") { }

        public BitmexResponseParseError(string message)
            : base(message) { }
    }

    [Serializable]
    public class BitmexUnexpectedResponse : Exception
    {
        public BitmexUnexpectedResponse()
            : base("Response is not expected") { }

        public BitmexUnexpectedResponse(string message)
            : base(message) { }
    }

    [Serializable]
    public class BitmexUnexpectedArgument : Exception
    {
        public BitmexUnexpectedArgument()
            : base("Unexpected argument passed to the request method") { }

        public BitmexUnexpectedArgument(string message)
            : base(message) { }
    }

    [Serializable]
    public class BitmexAPIError : Exception
    {
        public int? ApiErrorCode { get; } // Error code which API returns

        public string ApiErrorName { get; } // Error name which API returns

        public string ApiErrorMessage { get; } // Error message which API returns

        public BitmexAPIError(string message, int? errorCode = null, string errorName = "", string errorMessage = "")
            : base(message)
        {
            ApiErrorCode = errorCode;
            ApiErrorName = errorName;
            ApiErrorMessage = errorMessage;
        }
    }
}
