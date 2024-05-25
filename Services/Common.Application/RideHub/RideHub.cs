using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

public class RideHub : Hub
{
    public async Task SendMessageToDriver(string driverId, string message)
    {
        await Clients.User(driverId).SendAsync("ReceiveMessage", message);
    }

    public async Task UpdateDriverLocation(DriverLocationUpdateRequest request)
    {
        await Clients.All.SendAsync("newLocation", request);
    }
}
