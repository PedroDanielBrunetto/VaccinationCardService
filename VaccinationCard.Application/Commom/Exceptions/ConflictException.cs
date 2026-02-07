namespace VaccinationCard.Application.Commom.Exceptions
{
    public class ConflictException : Exception
    {
        public ConflictException(string message)
            : base(message) { }
    }

}
