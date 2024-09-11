using BackendTZ.Contracts.Requests;
using BackendTZ.Contracts.Response;
using BackendTZ.Core.Abstractions;
using BackendTZ.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace BackendTZ.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ServiceController : ControllerBase
    {
        private readonly IService<Service> _srvService;
        public ServiceController(IService<Service> srvService)
        {
            _srvService = srvService;
        }
        [HttpGet]
        public async Task<ActionResult<ServiceResponse>> GetAllServices()
        {
            var services = await _srvService.GetAll();
            var response = services.Select(s => new ServiceResponse(s.Id, s.Name, s.Price));
            return Ok(response);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse>> GetByIDService(Guid id)
        {
            var service = await _srvService.GetById(id);
            var response = new ServiceResponse(service.Id, service.Name, service.Price);
            return Ok(response);
        }
        [HttpPost]
        public async Task<ActionResult<Guid>> CreateService([FromBody] ServiceRequest request)
        {
            var (service, error) = Service.Create(
                Guid.NewGuid(),
                 request.name,
                 request.price
                );
            if (!string.IsNullOrEmpty(error))  return BadRequest(error);

            return Ok(await _srvService.Create(service));
        }
        [HttpPut("{id:guid}")]
        public async Task<ActionResult<Guid>> UpdateService(Guid id,  [FromBody] ServiceRequest request)
        {
            var updateService= Service.Create(
                 id,
                 request.name,
                 request.price
                ).Service;
            return Ok(await _srvService.Update(updateService));
        }
        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<Guid>> DeleteService(Guid id)
        {
            return Ok(await _srvService.Delete(id));
        }
    }
}
