namespace BackendTZ.Contracts.Response
{
    public record ServiceResponse(
        Guid id,
        string name,
        decimal price);

}