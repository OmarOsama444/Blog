using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Interfaces;

namespace Application.Services
{
    public class PresenceTrackerService : IPresenceTrackerService
    {
        public static readonly Dictionary<string, List<string>> onlineUsers = [];
        public Task<List<string>> GetConnectionsForUser(string userId)
        {
            List<string> result = [];
            lock (onlineUsers)
            {
                if (onlineUsers.TryGetValue(userId, out var connections))
                {
                    result = connections;
                }
            }
            return Task.FromResult(result);
        }

        public Task<List<string>> GetOnlineUsers()
        {
            List<string> result = [];
            lock (onlineUsers)
            {
                result = [.. onlineUsers.Keys];
            }
            return Task.FromResult(result);
        }

        public Task UserConnected(string userId, string connectionId)
        {
            lock (onlineUsers)
            {
                if (onlineUsers.TryGetValue(userId, out var connections))
                {
                    connections.Add(connectionId);
                }
                else
                {
                    onlineUsers[userId] = [connectionId];
                }
            }

            return Task.CompletedTask;
        }

        public Task UserDisconnected(string userId, string connectionId)
        {
            lock (onlineUsers)
            {
                if (!onlineUsers.TryGetValue(userId, out var connections)) return Task.CompletedTask;

                connections.Remove(connectionId);
                if (connections.Count == 0)
                    onlineUsers.Remove(userId);
            }
            return Task.CompletedTask;
        }
    }
}