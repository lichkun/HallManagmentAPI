using BackendTZ.Application.Services;
using BackendTZ.Contracts.Requests;
using BackendTZ.Contracts.Response;
using BackendTZ.Core.Abstractions;
using BackendTZ.Core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BackendTZ.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ConferenceHallController : ControllerBase
    {
        private readonly IService<ConferenceHall> _conferenceHallService;

        public ConferenceHallController(IService<ConferenceHall> conferenceHallService)
        {
            _conferenceHallService = conferenceHallService;
        }
        
        [HttpGet]
        public async Task<ActionResult<List<ConferenceHallResponse>>> GetAllConferenceHalls()
        {
            var conferenceHalls = await _conferenceHallService.GetAll();
            var response = conferenceHalls.Select(ch => new ConferenceHallResponse(
                ch.Id, ch.Name, ch.Capacity, ch.BaseRate, ch.ServicesIds));
            return Ok(response);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ConferenceHallResponse>> GetByIDHall(Guid id)
        {
            var hall = await _conferenceHallService.GetById(id);
            var response = new ConferenceHallResponse(hall.Id, hall.Name, hall.Capacity, hall.BaseRate, hall.ServicesIds);
            return Ok(response);
        }
        [HttpPost]
        public async Task<ActionResult<Guid>> CreateConferenceHall([FromBody] ConferenceHallRequest request)
        {
            var (conferenceHall, error) = ConferenceHall.Create(
                Guid.NewGuid(),
                request.Name,
                request.Capacity,
                request.BaseRate,
                request.ServicesIds
            );

            if (!string.IsNullOrEmpty(error)) return BadRequest(error);

            return Ok(await _conferenceHallService.Create(conferenceHall));
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<Guid>> UpdateConferenceHall(Guid id, [FromBody] ConferenceHallRequest request)
        {
            var updateConferenceHall = ConferenceHall.Create(
                id,
                request.Name,
                request.Capacity,
                request.BaseRate,
                request.ServicesIds
            ).hall;

            return Ok(await _conferenceHallService.Update(updateConferenceHall));
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<Guid>> DeleteConferenceHall(Guid id)
        {
            return Ok(await _conferenceHallService.Delete(id));
        }
    }
}
