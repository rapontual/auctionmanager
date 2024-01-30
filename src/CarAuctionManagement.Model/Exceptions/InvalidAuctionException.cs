namespace CarAuctionManagement.Model.Exceptions
{
    using System;

    public class InvalidAuctionException : Exception
    {
        private readonly string message;

        public InvalidAuctionException(string errorMessage)
        {
            message = errorMessage;
        }

        public override string Message
        {
            get { return message; }
        }
    }
}
