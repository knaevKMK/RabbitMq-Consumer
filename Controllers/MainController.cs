using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Producer.Models;

namespace Producer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MainController : ControllerBase
    {
        private readonly IBus _bus;
        public MainController(IBus bus)
        {
            _bus = bus;
        }
        [HttpPost]
        public async Task<IActionResult> CreateTicket(Meassge todoModel)
        {
            if (todoModel is not null)
            {
                Uri uri = new Uri("rabbitmq://localhost/Msg_Queue");
                var endPoint = await _bus.GetSendEndpoint(uri);
                await endPoint.Send(todoModel);
                Task.WaitAll();
                return Ok(todoModel.Content);
            }
            return BadRequest();
        }
    }
}
