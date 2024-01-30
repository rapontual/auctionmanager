namespace CarAuctionManagement.Model.Exceptions
{
    using System;

    public class InvalidBidException : Exception
    {
        private readonly string message;

        public InvalidBidException(double bid, int vehicleId)
        {
            message = $"The amout of {bid} is an invalid bid for the Vehicle with the identifier {vehicleId}";
        }

        public override string Message
        {
            get { return message; }
        }
    }
}
