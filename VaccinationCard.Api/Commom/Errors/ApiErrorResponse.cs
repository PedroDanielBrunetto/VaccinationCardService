namespace VaccinationCard.Api.Commom.Errors
{
    public class ApiErrorResponse
    {
        public int StatusCode { get; init; }
        public string Message { get; init; } = null!;
        public string? Details { get; init; }
    }
}
