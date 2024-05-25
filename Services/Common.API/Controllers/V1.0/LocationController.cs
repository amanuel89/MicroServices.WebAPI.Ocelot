//using Microsoft.AspNetCore.SignalR;
//using RideBackend.Application.Commands.PassengerCommand;

//namespace RideBackend.API.Controllers.V1._0
//{
//    [ApiController]
//    [Route("api/broadcast")]
//    public class BroadcastController : ControllerBase
//    {
//        private readonly IHubContext<LocationHub> _hub;

//        public BroadcastController(IHubContext<LocationHub> hub)
//        {
//            _hub = hub;
//        }

//        [HttpGet]
//        public async Task Get(string driverId, string location)
//        {
//            await _hub.Clients.All.SendAsync("ReceiveMessage", driverId, location);
//        }
//    }

//}
