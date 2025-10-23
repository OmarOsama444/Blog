using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IPresenceTrackerService
    {
        Task UserConnected(string userId, string connectionId);
        Task UserDisconnected(string userId, string connectionId);
        Task<List<string>> GetOnlineUsers();
        Task<List<string>> GetConnectionsForUser(string userId);
    }
}