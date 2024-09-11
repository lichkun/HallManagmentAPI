namespace BackendTZ.Contracts.Requests
{
    public record ConferenceHallRequest(
         string Name,
         int Capacity,
         decimal BaseRate,
         List<Guid> ServicesIds
        );
}
