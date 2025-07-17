using Microsoft.AspNetCore.SignalR;
using System.Collections.Concurrent;

namespace Api.Hubs
{
    public class WebRtcHub : Hub
    {
        // Tracks the first connection (broadcaster)
        private static string? _broadcasterConnectionId = null;

        // Track all active connections (optional, helpful for debugging / room logic)
        private static readonly ConcurrentDictionary<string, bool> _connections = new();

        public override async Task OnConnectedAsync()
        {
            var cid = Context.ConnectionId;
            _connections[cid] = true;

            // If no broadcaster yet, this one becomes broadcaster
            if (_broadcasterConnectionId == null)
            {
                _broadcasterConnectionId = cid;
                Console.WriteLine($"Broadcaster connected: {cid}");
                await Clients.Client(cid).SendAsync("YouAreBroadcaster");
            }
            else
            {
                Console.WriteLine($"Viewer connected: {cid}");
                // Notify the broadcaster a new viewer wants the stream
                await Clients.Client(_broadcasterConnectionId)
                    .SendAsync("ViewerJoined", cid);
                // Optionally tell the viewer who the broadcaster is
                await Clients.Client(cid).SendAsync("BroadcasterIs", _broadcasterConnectionId);
            }

            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            var cid = Context.ConnectionId;
            _connections.TryRemove(cid, out _);

            if (_broadcasterConnectionId == cid)
            {
                Console.WriteLine("Broadcaster disconnected.");
                _broadcasterConnectionId = null;

                // Notify all viewers that the stream ended
                await Clients.All.SendAsync("BroadcasterDisconnected");
            }

            await base.OnDisconnectedAsync(exception);
        }

        /// <summary>
        /// Broadcaster sends an SDP offer to a specific viewer.
        /// </summary>
        public async Task SendOffer(string targetConnectionId, string sdp)
        {
            Console.WriteLine($"SendOffer from {Context.ConnectionId} to {targetConnectionId}");
            await Clients.Client(targetConnectionId)
                .SendAsync("ReceiveOffer", sdp, Context.ConnectionId);
        }

        /// <summary>
        /// Viewer sends SDP answer back to broadcaster.
        /// </summary>
        public async Task SendAnswer(string targetConnectionId, string sdp)
        {
            Console.WriteLine($"SendAnswer from {Context.ConnectionId} to {targetConnectionId}");
            await Clients.Client(targetConnectionId)
                .SendAsync("ReceiveAnswer", sdp, Context.ConnectionId);
        }

        /// <summary>
        /// ICE candidates go between *two* peers only.
        /// </summary>
        public async Task SendIceCandidate(string targetConnectionId, string candidate)
        {
            Console.WriteLine($"SendIceCandidate from {Context.ConnectionId} to {targetConnectionId}");
            await Clients.Client(targetConnectionId)
                .SendAsync("ReceiveIceCandidate", candidate, Context.ConnectionId);
        }

    }
}
