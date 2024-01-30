namespace CarAuctionManagement.Model.Exceptions
{
    using System;

    public class DuplicateEntityException : Exception
    {
        private readonly string entityTypeName;
        private readonly string message;

        public DuplicateEntityException(string entityType) 
        {
            entityTypeName = entityType;
            message = $"An entity of {entityTypeName} type with the same identifier already exists";
        }

        public override string Message
        {
            get { return message; }
        }
    }
}
