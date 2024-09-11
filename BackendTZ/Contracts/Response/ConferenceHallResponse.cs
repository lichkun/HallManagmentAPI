namespace BackendTZ.Contracts.Response
{
    public record ConferenceHallResponse(
          Guid Id ,
         string Name,
         int Capacity,
         decimal BaseRate,
         List<Guid> ServicesIds
        );
}
