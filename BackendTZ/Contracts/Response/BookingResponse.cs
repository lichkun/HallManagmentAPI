namespace BackendTZ.Contracts.Response
{
    public record BookingResponse(
         Guid Id ,
         DateTime StartTime ,
         DateTime EndTime ,
         Guid ConferenceHallId ,
         List<Guid> ServicesIds ,
         decimal TotalPrice
        );
    
}
