using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;
using AncientBot.Core.UserProfiles;

namespace AncientBot.Core.LevelSys
{
    internal static class Lvl
    {
        internal static async void UserSentMessage(SocketGuildUser user, SocketTextChannel channel)
        {
            // if the user has a timeout, ignore them

            var userAccount = UserAccounts.GetAccount(user);
            uint oldLevel = userAccount.LevelNumber;
            userAccount.XP += 3;
            UserAccounts.SaveAccounts();
            uint newLevel = userAccount.LevelNumber;

            if (oldLevel != newLevel)
            {
                // the user leveled up
                var embed = new EmbedBuilder();
                embed.WithColor(0, 229, 255);
                embed.WithTitle("LEVEL GAINED!");
                embed.WithThumbnailUrl(user.GetAvatarUrl(ImageFormat.Auto));
                embed.WithDescription(user.Username + " just gained a level!");
                embed.AddField("LEVEL", newLevel);

                await channel.SendMessageAsync("", embed: embed.Build());
            }
        }
    }
}
