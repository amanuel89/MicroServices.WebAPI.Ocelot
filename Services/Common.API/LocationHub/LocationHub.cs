﻿using Microsoft.AspNetCore.SignalR;
public class SignalrHub : Hub
{
    //public async Task NewMessage(string user, string message)
    //{
    //    await Clients.All.SendAsync("messageReceived", user, message);
    //}

    public async Task UpdateDriverLocation(DriverLocationUpdateRequest request)
    {
        await Clients.All.SendAsync("newLocation", request);
    }
}