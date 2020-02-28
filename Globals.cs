using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord.WebSocket;

namespace AncientBot
{
    internal static class Globals
    {
        internal static DiscordSocketClient Client { get; set; }
        internal static ulong MessageIdToTrack { get; set; }
    }
}
