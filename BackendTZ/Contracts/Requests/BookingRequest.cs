namespace BackendTZ.Contracts.Requests
{
    public record BookingRequest(
        DateTime StartTime,
        DateTime EndTime,
        Guid ConferenceHallId,
        List<Guid> ServicesIds
    );
}
