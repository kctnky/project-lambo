using System;

namespace ProjectLambo.Okex
{
    [Serializable]
    public class OkexNoResponse : Exception
    {
        public OkexNoResponse()
            : base("Request returned empty") { }

        public OkexNoResponse(string message)
            : base(message) { }
    }

    [Serializable]
    public class OkexResponseParseError : Exception
    {
        public OkexResponseParseError()
            : base("Response couldn't be parsed as expected") { }

        public OkexResponseParseError(string message)
            : base(message) { }
    }

    [Serializable]
    public class OkexUnexpectedResponse : Exception
    {
        public OkexUnexpectedResponse()
            : base("Response is not expected") { }

        public OkexUnexpectedResponse(string message)
            : base(message) { }
    }

    [Serializable]
    public class OkexUnexpectedArgument : Exception
    {
        public OkexUnexpectedArgument()
            : base("Unexpected argument passed to the request method") { }

        public OkexUnexpectedArgument(string message)
            : base(message) { }
    }

    [Serializable]
    public class OkexAPIError : Exception
    {
        public int? ApiErrorCode { get; } // Error code which API returns

        public string ApiErrorName { get; } // Error name which API returns

        public string ApiErrorMessage { get; } // Error message which API returns

        public OkexAPIError(string message, int? errorCode = null, string errorName = "", string errorMessage = "")
            : base(message)
        {
            ApiErrorCode = errorCode;
            ApiErrorName = errorName;
            ApiErrorMessage = errorMessage;
        }
    }
}
