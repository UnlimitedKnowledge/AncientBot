using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using Discord;
using Discord.Commands;
using Discord.Rest;
using Discord.WebSocket;
using System.Net;
using Discord.Addons.InteractiveCommands;
using NReco.ImageGenerator;
using AncientBot.Core.UserProfiles;
using AncientBot;
using AncientBot.Core;
using ImageFormat = Discord.ImageFormat;

namespace AncientBot.Modules
{
    public class Commands : ModuleBase<SocketCommandContext>
    {





        // --------------------------------FUN COMMANDS----------------------------------------


        [Command("say")]
        public async Task Echo([Remainder]string message)
        {
            var embed = new EmbedBuilder();
            embed.WithTitle(Context.Client.CurrentUser.Username + " Says...");
            embed.WithThumbnailUrl(Context.Client.CurrentUser.GetAvatarUrl(ImageFormat.Auto));
            embed.WithDescription(message);
            embed.WithColor(new Color(0, 255, 0));

            await Context.Channel.SendMessageAsync("", false, embed.Build());
        }


        [Command("hello")]
        public async Task Hello()
        {
            var embed = new EmbedBuilder();
            embed.WithTitle(Context.Client.CurrentUser.Username);
            embed.WithThumbnailUrl(Context.Client.CurrentUser.GetAvatarUrl(ImageFormat.Auto));
            embed.WithDescription("Hello," + " " + Context.User.Username + "!" + " " + "Welcome to " + Context.Guild.Name);
            embed.WithCurrentTimestamp();
            embed.WithColor(new Color(0, 255, 0));

            await Context.Channel.SendMessageAsync("", false, embed.Build());
        }

        [Command("choose")]
        public async Task PickOne([Remainder]string message)
        {
            string[] options = message.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);

            Random r = new Random();
            string selection = options[r.Next(0, options.Length)];

            var embed = new EmbedBuilder();
            embed.WithTitle(Context.Client.CurrentUser.Username + " chooses...");
            embed.WithThumbnailUrl(Context.Client.CurrentUser.GetAvatarUrl(ImageFormat.Auto));
            embed.WithDescription(selection);
            embed.WithColor(new Color(255, 255, 0));

            await Context.Channel.SendMessageAsync("", false, embed.Build());

        }

        [Command("getinfo")]
        public async Task myStats([Remainder]string arg = "")
        {
            SocketUser target = null;
            var mentionedUser = Context.Message.MentionedUsers.FirstOrDefault();
            target = mentionedUser ?? Context.User;

            var account = UserAccounts.GetAccount(target);


            var embed = new EmbedBuilder();
            embed.WithTitle(target.Username + "'s Information");
            embed.WithColor(69, 69, 69);
            embed.WithCurrentTimestamp();
            embed.WithThumbnailUrl(target.GetAvatarUrl(ImageFormat.Auto));
            embed.AddField("Username", target.Username);
            embed.AddField("Level", account.LevelNumber);
            embed.AddField("Warnings", account.NumberOfWarnings);
            // embed.AddField("Warning 1 ", account.Reason);
            // embed.AddField("Warning 2 ", account.Reason2);
            // embed.AddField("Warning 3 ", account.Reason3);
            // embed.AddField("Warning 4+", account.Newest);
            embed.AddField("Mute Status", account.IsMuted);
            embed.AddField("Report Blacklist", account.ReportBlacklist);
            embed.AddField("Suggestion Blacklist", account.SuggestBlacklist);
            embed.AddField("Bot Blacklist", account.Blacklisted);
            embed.AddField("Status", target.Status);
            embed.AddField("User ID", target.Id);
            embed.AddField("Account creation", target.CreatedAt);
            embed.WithFooter("Built by Bay#6969", null);

            await Context.Channel.SendMessageAsync("", false, embed.Build());
        }

        [Command("staffinfo")]
        public async Task StaffStats([Remainder]string arg = "")
        {
            SocketUser target = null;
            var mentionedUser = Context.Message.MentionedUsers.FirstOrDefault();
            target = mentionedUser ?? Context.User;

            var account = UserAccounts.GetAccount(target);


            var embed = new EmbedBuilder();
            embed.WithTitle("Staff Review");
            embed.WithColor(255, 255, 0);
            embed.WithCurrentTimestamp();
            embed.WithThumbnailUrl(target.GetAvatarUrl(ImageFormat.Auto));
            embed.AddField("Username", target.Username);
            embed.AddField("Strikes", account.NumberOfStrikes);
            embed.AddField("Strike 1 ", account.Strike);
            embed.AddField("Strike 2 ", account.Strike2);
            embed.AddField("Strike 3 ", account.Strike3);
            embed.AddField("Strike 4+ ", account.NewestS);
            embed.AddField("Status", target.Status);
            embed.AddField("User ID", target.Id);
            embed.AddField("Account creation", target.CreatedAt);
            embed.WithFooter("Built by Bay#6969", null);

            await Context.Channel.SendMessageAsync("", false, embed.Build());
        }

        [Group("ping")]
        public class Ping : ModuleBase<SocketCommandContext>
        {
            [Command]
            public async Task DefaultPing()
            {
                DiscordSocketClient _client;
                _client = new DiscordSocketClient();

                await ReplyAsync("Pong!");
            }
        }

        [Group("avatar")]
        public class avatar : ModuleBase<SocketCommandContext>
        {
            [Command]
            public async Task avatarinfo([Remainder]string arg = "")
            {
                SocketUser target = null;
                var mentionedUser = Context.Message.MentionedUsers.FirstOrDefault();
                target = mentionedUser ?? Context.User;

                await ReplyAsync(target.GetAvatarUrl(ImageFormat.Auto));
            }
        }


        [Command("serverinfo")]
        public async Task server()
        {



            var embed = new EmbedBuilder();
            embed.WithTitle(Context.Guild.Name);
            embed.WithColor(0, 0, 255);
            embed.WithCurrentTimestamp();
            embed.WithThumbnailUrl(Context.Guild.IconUrl);
            embed.AddField("Server Name", Context.Guild.Name);
            embed.AddField("Server ID", Context.Guild.Id);
            embed.AddField("Server Owner", Context.Guild.Owner);
            embed.AddField("Member Count", Context.Guild.MemberCount);
            embed.AddField("Region", Context.Guild.VoiceRegionId);
            embed.AddField("Channel Count", Context.Guild.Channels.Count);
            embed.AddField("Voice Channel Count", Context.Guild.VoiceChannels.Count);
            embed.AddField("Verification Level", Context.Guild.VerificationLevel);
            embed.AddField("Server Icon", Context.Guild.IconUrl);
            embed.WithFooter("Built by Bay#6969", null);

            await Context.Channel.SendMessageAsync("", false, embed.Build());
        }


        [Command("help")]
        public async Task Help()
        {
            var embed = new EmbedBuilder();
            embed.WithTitle("Command Help");
            embed.WithColor(0, 255, 0);
            embed.WithCurrentTimestamp();
            embed.WithThumbnailUrl(Context.Client.CurrentUser.GetAvatarUrl(ImageFormat.Auto));
            embed.AddField("help", "Displays what you're seeing right now.");
            embed.AddField("say", "Repeats what you type.");
            embed.AddField("hello", "Greets you with a join greeting.");
            embed.AddField("choose", "Chooses between two options. Ex. choose Blue|Red.");
            embed.AddField("getinfo", "Gathers information about a user's profile.");
            embed.AddField("serverinfo", "Displays information for the server.");
            embed.AddField("ping", "You thought this was gonna return the connection! No.");
            embed.AddField("avatar", "Grabs the profile picture of the user.");
            embed.AddField("modhelp", "Display commands for moderators.");
            embed.AddField("ancienthelp", "Display commands relevant to TAE.");
            embed.AddField("ownerhelp", "Display commands for the bot owner.");
            embed.WithFooter("Built by Bay#6969", null);

            await Context.Channel.SendMessageAsync("", false, embed.Build());
        }

        [Command("modhelp")]
        public async Task modcmds()
        {
            var embed = new EmbedBuilder();
            embed.WithTitle("Moderation Help");
            embed.WithColor(0, 255, 0);
            embed.WithCurrentTimestamp();
            embed.WithThumbnailUrl(Context.Client.CurrentUser.GetAvatarUrl(ImageFormat.Auto));
            embed.AddField("kick", "Kick the user from the server.");
            embed.AddField("ban", "Bans the user from the server.");
            embed.AddField("unban", "Unbans the user from the server. (Use User ID)");
            embed.AddField("warn", "Warns the user.");
            embed.AddField("clearwarn", "Clears the warnings of the user.");
            embed.AddField("strike", "Strike the user.");
            embed.AddField("clearstrike", "Clears the strikes of the user.");
            embed.AddField("mute", "Mutes the user permanently.");
            embed.AddField("unmute", "Unmutes the user.");
            embed.AddField("purge", "Deletes however many messages are stated.");
            embed.AddField("nickname", "Changes the user's nickname.");
            embed.AddField("warnings", "Shows the warnings and reasons for the user.");
            embed.AddField("strikes", "Shows the strikes and reasons for the user.");
            embed.AddField("staffinfo", "Gives a rundown of the staff member's infractions.");
            embed.WithFooter("Built by Bay#6969", null);

            await Context.Channel.SendMessageAsync("", false, embed.Build());
        }

        [Command("ownerhelp")]
        public async Task ownercmds()
        {

            if (Context.User.Id == 209187780079648778)
            {
                var embed = new EmbedBuilder();
                embed.WithTitle("Owner Help");
                embed.WithColor(0, 255, 0);
                embed.WithCurrentTimestamp();
                embed.WithThumbnailUrl(Context.User.GetAvatarUrl(ImageFormat.Auto));
                embed.AddField("data", "Helpful information for debugging.");
                embed.AddField("shutdown", "Shuts down the bot, can not be restarted.");
                embed.AddField("restart", "Restarts the bot while it is running.");
                embed.AddField("cleardata", "Clears the user data and then restarts the bot.");
                embed.AddField("blacklist", "Blacklists a user from using commands.");
                embed.AddField("unblacklist", "Revokes the blacklist.");
                embed.AddField("setgame", "Sets the game status of the bot.");
                embed.AddField("setavatar", "Changes the bot's profile picture.");
                embed.WithFooter("Built by Bay#6969", null);

                await Context.Channel.SendMessageAsync("", false, embed.Build());
            }

            else
            {
                var embed = new EmbedBuilder();
                embed.WithTitle("Command Failed");
                embed.WithColor(0, 255, 0);
                embed.WithCurrentTimestamp();
                embed.WithDescription("You are not the owner of the bot.");
                embed.WithFooter("Built by Bay#6969", null);

                await Context.Channel.SendMessageAsync("", false, embed.Build());
            }

        }


        [Command("ancienthelp", RunMode = RunMode.Async)]
        public async Task help()
        {


            var embed = new EmbedBuilder();
            embed.WithTitle("Ancient Help");
            embed.WithColor(0, 255, 0);
            embed.WithCurrentTimestamp();
            embed.WithThumbnailUrl(Context.Client.CurrentUser.GetAvatarUrl(ImageFormat.Auto));
            embed.AddField("logpr", "Log a PR.");
            embed.AddField("addexploiter", "Adds an exploiter to the exploit log.");
            embed.AddField("report", "Send a report.");
            embed.AddField("suggest", "Send a suggestion.");
            embed.AddField("affiliate", "Send an affiliation request.");
            embed.AddField("rblacklist", "Blacklists a user from using report.");
            embed.AddField("unrblacklist", "Removes the report blacklist.");
            embed.AddField("sblacklist", "Blacklists a user from using suggest.");
            embed.AddField("unsblacklist", "Removes the suggestion blacklist.");
            embed.WithFooter("Built by Bay#6969", null);

            await Context.Channel.SendMessageAsync("", false, embed.Build());


        }


        // --------------------------------FUN COMMANDS----------------------------------------

        // --------------------------------MODERATION COMMANDS----------------------------------------

        [Command("kick")]
        [RequireBotPermission(GuildPermission.KickMembers)]
        public async Task KickUser(IGuildUser user, [Remainder]string reason = "No reason provided.")
        {
            var account = UserAccounts.GetAccount(Context.User);

            var conuser = Context.User as SocketGuildUser;
            var role = (user as IGuildUser).Guild.Roles.FirstOrDefault(x => x.Name == "Community Moderation Team");


            if (!conuser.Roles.Contains(role))
            {
                var embed = new EmbedBuilder();
                embed.WithTitle("Kick Denied");
                embed.WithDescription("You do not have the proper permissions to use this command.");
                embed.WithCurrentTimestamp();
                embed.WithColor(255, 0, 0);
                embed.WithFooter("Built by Bay#6969", null);

                await Context.Channel.SendMessageAsync("", false, embed.Build());
            }

            if (conuser.Roles.Contains(role))
            {
                var embed = new EmbedBuilder();
                embed.WithTitle(user + " has been kicked by " + Context.User + ".");
                embed.WithColor(0, 255, 0);
                embed.WithDescription("**Reason: **" + reason + "\n **Moderator: **" + Context.User);
                embed.WithCurrentTimestamp();
                embed.WithThumbnailUrl(user.GetAvatarUrl(ImageFormat.Auto));
                embed.WithFooter("Built by Bay#6969", null);

                await Context.Channel.SendMessageAsync("", false, embed.Build());

                var dmChannel = await user.GetOrCreateDMChannelAsync();
                await dmChannel.SendMessageAsync("You have been kicked from " + Context.Guild.Name + "\n **Reason: " + reason + "**");
                await user.KickAsync(reason);
            }
        }

        [Command("ban")]
        [RequireBotPermission(GuildPermission.BanMembers)]
        public async Task BanUser(IGuildUser user, [Remainder]string reason = "No reason provided.")
        {

            var account = UserAccounts.GetAccount(Context.User);

            var conuser = Context.User as SocketGuildUser;
            var role = (user as IGuildUser).Guild.Roles.FirstOrDefault(x => x.Name == "Community Administration Team");

            if (!conuser.Roles.Contains(role))
            {
                var embed = new EmbedBuilder();
                embed.WithTitle("Ban Denied");
                embed.WithDescription("You do not have the proper permissions to use this command.");
                embed.WithCurrentTimestamp();
                embed.WithColor(255, 0, 0);
                embed.WithFooter("Built by Bay#6969", null);

                await Context.Channel.SendMessageAsync("", false, embed.Build());

            }
            else if (conuser.Roles.Contains(role))
            {
                var embed = new EmbedBuilder();
                embed.WithTitle(user + " has been banned by " + Context.User + ".");
                embed.WithColor(0, 255, 0);
                embed.WithDescription("**Reason: **" + reason + "\n **Moderator: **" + Context.User);
                embed.WithCurrentTimestamp();
                embed.WithThumbnailUrl(user.GetAvatarUrl(ImageFormat.Auto));
                embed.WithFooter("Built by Bay#6969", null);

                await Context.Channel.SendMessageAsync("", false, embed.Build());

                var dmChannel = await user.GetOrCreateDMChannelAsync();
                await dmChannel.SendMessageAsync("You have been banned from " + Context.Guild.Name + "\n **Reason: ** " + reason + "\n *Moderator: **" + Context.User);

                await user.Guild.AddBanAsync(user, 7, reason);
            }
        }

        [Command("unban")]
        [RequireBotPermission(GuildPermission.BanMembers)]
        public async Task UnBanUser(ulong userId)
        {


            var user = Context.User as SocketGuildUser;
            var role = (user as IGuildUser).Guild.Roles.FirstOrDefault(x => x.Name == "Community Administration Team");

            if (!user.Roles.Contains(role))
            {
                var embed = new EmbedBuilder();
                embed.WithTitle("Ban Denied");
                embed.WithDescription("You do not have the proper permissions to use this command.");
                embed.WithCurrentTimestamp();
                embed.WithColor(255, 0, 0);
                embed.WithFooter("Built by Bay#6969", null);

                await Context.Channel.SendMessageAsync("", false, embed.Build());

            }

            else if (user.Roles.Contains(role))
            {
                var embed = new EmbedBuilder();
                embed.WithTitle("(" + userId + ") has been unbanned by " + Context.User + ".");
                embed.WithColor(0, 255, 0);
                embed.WithDescription("Moderator: " + Context.User);
                embed.WithCurrentTimestamp();
                embed.WithFooter("Built by Bay#6969", null);

                await Context.Channel.SendMessageAsync("", false, embed.Build());

                await Context.Guild.RemoveBanAsync(userId);
            }



        }

        [Command("warn")]
        [RequireUserPermission(GuildPermission.ManageMessages)]
        [RequireBotPermission(GuildPermission.Administrator)]
        public async Task WarnUser(IGuildUser user, [Remainder]string reason)
        {
            var userAccount = UserAccounts.GetAccount((SocketUser)user);
            userAccount.NumberOfWarnings++;
            UserAccounts.SaveAccounts();
            userAccount.User = user.Username;
            userAccount.Nick = user.Nickname;


            var dmChannel = await user.GetOrCreateDMChannelAsync();

            if (userAccount.NumberOfWarnings == 1)
            {

                SocketUser target = null;
                var mentionedUser = Context.Message.MentionedUsers.FirstOrDefault();
                target = mentionedUser ?? Context.User;

                var account = UserAccounts.GetAccount(target);

                account.Reason = reason;

                var embed = new EmbedBuilder();
                embed.WithTitle(user + " has been warned by " + Context.User + ".");
                embed.WithColor(0, 255, 0);
                embed.WithDescription("**Reason: **" + reason + "\n **Moderator: **" + Context.User + "\n **Warnings: **" + userAccount.NumberOfWarnings);
                embed.WithCurrentTimestamp();
                embed.WithThumbnailUrl(user.GetAvatarUrl(ImageFormat.Auto));
                embed.WithFooter("Built by Bay#6969", null);

                await Context.Channel.SendMessageAsync("", false, embed.Build());

                await dmChannel.SendMessageAsync("You have been warned in " + Context.Guild.Name + "\n **Reason:** " + reason + "\n **Moderator: **" + Context.User + "\n Warning #" + userAccount.NumberOfWarnings + ".");

            }

            if (userAccount.NumberOfWarnings == 2)
            {
                SocketUser target = null;
                var mentionedUser = Context.Message.MentionedUsers.FirstOrDefault();
                target = mentionedUser ?? Context.User;

                var account = UserAccounts.GetAccount(target);


                account.Reason2 = reason;



                var embed = new EmbedBuilder();
                embed.WithTitle(user + " has been warned by " + Context.User + ".");
                embed.WithColor(0, 255, 0);
                embed.WithDescription("**Reason: **" + reason + "\n **Moderator: **" + Context.User + "\n **Warnings: **" + userAccount.NumberOfWarnings);
                embed.WithCurrentTimestamp();
                embed.WithThumbnailUrl(user.GetAvatarUrl(ImageFormat.Auto));
                embed.WithFooter("Built by Bay#6969", null);

                await Context.Channel.SendMessageAsync("", false, embed.Build());

                await dmChannel.SendMessageAsync("You have been warned in " + Context.Guild.Name + "\n **Reason:** " + reason + "\n **Moderator: **" + Context.User + "\n Warning #" + userAccount.NumberOfWarnings + ".");
                reason = null;
            }


            if (userAccount.NumberOfWarnings == 3)
            {

                SocketUser target = null;
                var mentionedUser = Context.Message.MentionedUsers.FirstOrDefault();
                target = mentionedUser ?? Context.User;

                var account = UserAccounts.GetAccount(target);
                account.Reason3 = reason;
                UserAccounts.SaveAccounts();


                var embed = new EmbedBuilder();
                embed.WithTitle(user + " has been warned by " + Context.User + ".");
                embed.WithColor(0, 255, 0);
                embed.WithDescription("**Reason: **" + reason + "\n **Moderator: ** " + Context.User + "\n **Warnings: **" + userAccount.NumberOfWarnings);
                embed.WithCurrentTimestamp();
                embed.WithThumbnailUrl(user.GetAvatarUrl(ImageFormat.Auto));
                embed.WithFooter("Built by Bay#6969", null);

                await Context.Channel.SendMessageAsync("", false, embed.Build());

                await Context.Channel.SendMessageAsync("<@&666463669537996852>, <@&501911978818666506>, <@&503390540402917378> | Review triggered for, " + user.Mention + ", due to three or more warnings.");


                var embed2 = new EmbedBuilder();
                embed2.WithTitle(target.Username + "'s Information");
                embed2.WithColor(255, 0, 0);
                embed2.WithCurrentTimestamp();
                embed2.WithThumbnailUrl(target.GetAvatarUrl(ImageFormat.Auto));
                embed2.AddField("Username", target.Username);
                embed2.AddField("Level", account.LevelNumber);
                embed2.AddField("Warnings", account.NumberOfWarnings);
                embed2.AddField("Warning 1 ", account.Reason);
                embed2.AddField("Warning 2 ", account.Reason2);
                embed2.AddField("Warning 3 ", account.Reason3);
                embed2.AddField("Warning 4+ ", account.Newest);
                embed2.AddField("Mute Status", account.IsMuted);
                embed2.AddField("Status", target.Status);
                embed2.AddField("User ID", target.Id);
                embed2.AddField("Account creation", target.CreatedAt);
                embed.WithFooter("Built by Bay#6969", null);

                await Context.Channel.SendMessageAsync("", false, embed2.Build());

                await dmChannel.SendMessageAsync("You have reached three or more warnings in " + Context.Guild.Name + ". A review is now underway by our moderation team." + "**\n Reason: ** " + reason + "\n **Moderator: ** " + Context.User + ".");
            }

            if (userAccount.NumberOfWarnings >= 4)
            {

                SocketUser target = null;
                var mentionedUser = Context.Message.MentionedUsers.FirstOrDefault();
                target = mentionedUser ?? Context.User;

                var account = UserAccounts.GetAccount(target);


                account.Newest = reason;



                var embed = new EmbedBuilder();
                embed.WithTitle(user + " has been warned by " + Context.User + ".");
                embed.WithColor(0, 255, 0);
                embed.WithDescription("**Reason: **" + reason + "\n **Moderator: **" + Context.User + "\n **Warnings: **" + userAccount.NumberOfWarnings);
                embed.WithCurrentTimestamp();
                embed.WithThumbnailUrl(user.GetAvatarUrl(ImageFormat.Auto));
                embed.WithFooter("Built by Bay#6969", null);

                await Context.Channel.SendMessageAsync("", false, embed.Build());

                await dmChannel.SendMessageAsync("You have been warned in " + Context.Guild.Name + "\n **Reason:** " + reason + "\n **Moderator: **" + Context.User + "\n Warning #" + userAccount.NumberOfWarnings + ".");
                reason = null;

            }


        }

        [Command("clearwarn")]
        [RequireUserPermission(GuildPermission.Administrator)]
        [RequireBotPermission(GuildPermission.Administrator)]
        public async Task warnclear([Remainder]string arg = "")
        {

            SocketUser target = null;
            var mentionedUser = Context.Message.MentionedUsers.FirstOrDefault();
            target = mentionedUser ?? Context.User;

            var userAccount = UserAccounts.GetAccount((SocketUser)target);
            userAccount.NumberOfWarnings = 0;

            var account = UserAccounts.GetAccount(target);
            account.Reason = "N/A";
            account.Reason2 = "N/A";
            account.Reason3 = "N/A";
            account.Newest = "N/A";
            UserAccounts.SaveAccounts();

            var embed = new EmbedBuilder();
            embed.WithTitle(target + "'s warnings have been cleared");
            embed.WithDescription("**Cleared by: **" + Context.User + ".");
            embed.WithColor(0, 255, 0);
            embed.WithCurrentTimestamp();
            embed.WithThumbnailUrl(target.GetAvatarUrl(ImageFormat.Auto));
            embed.WithFooter("Built by Bay#6969", null);

            await Context.Channel.SendMessageAsync("", false, embed.Build());
        }

        [Command("strike")]
        [RequireUserPermission(GuildPermission.Administrator)]
        [RequireBotPermission(GuildPermission.Administrator)]
        public async Task StrikeUser(IGuildUser user, [Remainder]string reason)

        {

            SocketUser target = null;
            var mentionedUser = Context.Message.MentionedUsers.FirstOrDefault();
            target = mentionedUser ?? Context.User;

            var account = UserAccounts.GetAccount(target);

            var dmChannel = await user.GetOrCreateDMChannelAsync();

            var userAccount = UserAccounts.GetAccount((SocketUser)user);
            userAccount.NumberOfStrikes++;
            userAccount.StrikeReason = reason;
            userAccount.User = user.Username;
            userAccount.Nick = user.Nickname;
            UserAccounts.SaveAccounts();

            var embed = new EmbedBuilder();
            embed.WithTitle(user + " has been striked by " + Context.User + ".");
            embed.WithColor(0, 255, 0);
            embed.WithDescription("**Reason: **" + reason + "\n**Administrator: **" + Context.User + "\n**Strikes: **" + userAccount.NumberOfStrikes);
            embed.WithCurrentTimestamp();
            embed.WithThumbnailUrl(user.GetAvatarUrl(ImageFormat.Auto));
            embed.WithFooter("Built by Bay#6969", null);

            await Context.Channel.SendMessageAsync("", false, embed.Build());

            await dmChannel.SendMessageAsync("You have been striked in " + Context.Guild.Name + "\n**Reason:** " + reason + "\n**Administrator: **" + Context.User + "\nStrike #" + userAccount.NumberOfStrikes + ".");


            if (userAccount.NumberOfStrikes == 1)
            {
                account.Strike = reason;
            }

            if (userAccount.NumberOfStrikes == 2)
            {
                account.Strike2 = reason;
            }

            if (userAccount.NumberOfStrikes == 3)
            {
                account.Strike3 = reason;
            }

            if (userAccount.NumberOfStrikes >= 4)
            {
                account.NewestS = reason;
            }

        }

        [Command("clearstrike")]
        [RequireUserPermission(GuildPermission.Administrator)]
        [RequireBotPermission(GuildPermission.Administrator)]
        public async Task strikeclear([Remainder]string arg = "")
        {

            SocketUser target = null;
            var mentionedUser = Context.Message.MentionedUsers.FirstOrDefault();
            target = mentionedUser ?? Context.User;

            var userAccount = UserAccounts.GetAccount((SocketUser)target);
            userAccount.NumberOfStrikes = 0;

            var account = UserAccounts.GetAccount(target);
            account.Strike = "N/A";
            account.Strike2 = "N/A";
            account.Strike3 = "N/A";
            account.NumberOfStrikes = 0;
            account.NewestS = "N/A";
            UserAccounts.SaveAccounts();

            var embed = new EmbedBuilder();
            embed.WithTitle(target + "'s strikes have been cleared");
            embed.WithDescription("**Cleared by: **" + Context.User + ".");
            embed.WithColor(0, 255, 0);
            embed.WithCurrentTimestamp();
            embed.WithThumbnailUrl(target.GetAvatarUrl(ImageFormat.Auto));
            embed.WithFooter("Built by Bay#6969", null);

            await Context.Channel.SendMessageAsync("", false, embed.Build());
        }

        [Command("mute")]
        [RequireUserPermission(GuildPermission.ManageMessages)]
        [RequireBotPermission(GuildPermission.ManageRoles)]
        public async Task muteuser(IGuildUser user, [Remainder]string reason)
        {

            if (user.GuildPermissions.ManageMessages)
            {
                var embed = new EmbedBuilder();
                embed.WithTitle("Mute Denied");
                embed.WithDescription(user.Username + "'s permissions are higher than or equal to yours.");
                embed.WithCurrentTimestamp();
                embed.WithColor(255, 0, 0);
                embed.WithThumbnailUrl(user.GetAvatarUrl(ImageFormat.Auto));
                embed.WithFooter("Built by Bay#6969", null);

                await Context.Channel.SendMessageAsync("", false, embed.Build());
            }
            else
            {
                var embed = new EmbedBuilder();
                embed.WithTitle(user.Username + " has been muted by " + Context.User + ".");
                embed.WithColor(0, 255, 0);
                embed.WithDescription("**Reason: **" + reason + "\n**Moderator: **" + Context.User);
                embed.WithCurrentTimestamp();
                embed.WithThumbnailUrl(user.GetAvatarUrl(ImageFormat.Auto));
                embed.WithFooter("Built by Bay#6969", null);

                SocketUser target = null;
                var mentionedUser = Context.Message.MentionedUsers.FirstOrDefault();
                target = mentionedUser ?? Context.User;

                var account = UserAccounts.GetAccount(target);

                account.IsMuted = true;

                await Context.Channel.SendMessageAsync("", false, embed.Build());

                var role = Context.Guild.Roles.FirstOrDefault(x => x.Name == "Muted");
                await (user as IGuildUser).AddRoleAsync(role);

                var dmChannel = await user.GetOrCreateDMChannelAsync();
                await dmChannel.SendMessageAsync("You have been muted in " + Context.Guild.Name + "\n **Reason: " + reason + "**");

            }
        }


        [Command("unmute")]
        [RequireUserPermission(GuildPermission.ManageMessages)]
        [RequireBotPermission(GuildPermission.ManageRoles)]
        public async Task unmuteuser(IGuildUser user)
        {
            if (user.GuildPermissions.ManageMessages)
            {
                var embed = new EmbedBuilder();
                embed.WithTitle("Unmute Denied");
                embed.WithDescription(user.Nickname + "'s permissions are higher than or equal to yours.");
                embed.WithCurrentTimestamp();
                embed.WithColor(255, 0, 0);
                embed.WithThumbnailUrl(user.GetAvatarUrl(ImageFormat.Auto));
                embed.WithFooter("Built by Bay#6969", null);

                await Context.Channel.SendMessageAsync("", false, embed.Build());
            }
            else
            {
                var embed = new EmbedBuilder();
                embed.WithTitle(user + " has been unmuted by " + Context.User + ".");
                embed.WithColor(0, 255, 0);
                embed.WithCurrentTimestamp();
                embed.WithThumbnailUrl(user.GetAvatarUrl(ImageFormat.Auto));
                embed.WithFooter("Built by Bay#6969", null);

                await Context.Channel.SendMessageAsync("", false, embed.Build());

                SocketUser target = null;
                var mentionedUser = Context.Message.MentionedUsers.FirstOrDefault();
                target = mentionedUser ?? Context.User;

                var account = UserAccounts.GetAccount(target);

                account.IsMuted = false;

                var role = Context.Guild.Roles.FirstOrDefault(x => x.Name == "Muted");
                await (user as IGuildUser).RemoveRoleAsync(role);

                var dmChannel = await user.GetOrCreateDMChannelAsync();
                await dmChannel.SendMessageAsync("You have been unmuted in " + Context.Guild.Name);

            }
        }


        [Command("warnings")]
        [RequireBotPermission(GuildPermission.SendMessages)]
        public async Task warnshow([Remainder]string arg = "")
        {

            SocketUser target = null;
            var mentionedUser = Context.Message.MentionedUsers.FirstOrDefault();
            target = mentionedUser ?? Context.User;

            var userAccount = UserAccounts.GetAccount((SocketUser)target);
            var account = UserAccounts.GetAccount(target);


            var embed = new EmbedBuilder();
            embed.WithTitle(target + "'s Warnings");
            embed.AddField("Warning 1 ", account.Reason);
            embed.AddField("Warning 2 ", account.Reason2);
            embed.AddField("Warning 3 ", account.Reason3);
            embed.AddField("Warning 4+ ", account.Newest);
            embed.AddField("Warning Count ", account.NumberOfWarnings);
            embed.WithThumbnailUrl(target.GetAvatarUrl(ImageFormat.Auto));
            embed.WithColor(255, 0, 0);
            embed.WithCurrentTimestamp();
            embed.WithFooter("Built by Bay#6969", null);

            await Context.Channel.SendMessageAsync("", false, embed.Build());
        }

        [Command("strikes")]
        [RequireBotPermission(GuildPermission.SendMessages)]
        public async Task strikeshow([Remainder]string arg = "")
        {
            SocketUser target = null;
            var mentionedUser = Context.Message.MentionedUsers.FirstOrDefault();
            target = mentionedUser ?? Context.User;

            var userAccount = UserAccounts.GetAccount((SocketUser)target);
            var account = UserAccounts.GetAccount(target);

            var embed = new EmbedBuilder();
            embed.WithTitle(target + "'s Strikes");
            embed.AddField("Strike 1 ", account.Strike);
            embed.AddField("Strike 2 ", account.Strike2);
            embed.AddField("Strike 3 ", account.Strike3);
            embed.AddField("Strike 4+ ", account.NewestS);
            embed.WithThumbnailUrl(target.GetAvatarUrl(ImageFormat.Auto));
            embed.WithColor(255, 0, 0);
            embed.WithCurrentTimestamp();
            embed.WithFooter("Built by Bay#6969", null);

            await Context.Channel.SendMessageAsync("", false, embed.Build());
        }

        [Command("nickname")]
        [RequireUserPermission(GuildPermission.ManageNicknames)]
        public async Task Nickname(SocketGuildUser username, [Remainder]string name)
        {

            if (username.GuildPermissions.ManageNicknames)
            {
                var embed = new EmbedBuilder();
                embed.WithTitle("Mute Denied");
                embed.WithDescription(username.Username + "'s permissions are higher than or equal to yours.");
                embed.WithCurrentTimestamp();
                embed.WithColor(255, 0, 0);
                embed.WithThumbnailUrl(username.GetAvatarUrl(ImageFormat.Auto));
                embed.WithFooter("Built by Bay#6969", null);

                await Context.Channel.SendMessageAsync("", false, embed.Build());
            }
            
            else
            {
                await Context.Guild.GetUser(username.Id).ModifyAsync(x => x.Nickname = name);

                SocketUser target = null;
                var mentionedUser = Context.Message.MentionedUsers.FirstOrDefault();
                target = mentionedUser ?? Context.User;

                var embed = new EmbedBuilder();
                embed.WithTitle("Nickname Set");
                embed.WithDescription(target.Username + "'s nickname has been set to " + name + ".");
                embed.WithThumbnailUrl(target.GetAvatarUrl(ImageFormat.Auto));
                embed.WithColor(new Color(0, 255, 0));

                await Context.Channel.SendMessageAsync("", false, embed.Build());
            }

        }


        [Command("purge", RunMode = RunMode.Async)]
        [RequireUserPermission(GuildPermission.ManageMessages)]
        public async Task Clear(int amountOfMessagesToDelete)
        {
            await (Context.Message.Channel as SocketTextChannel).DeleteMessagesAsync(await Context.Message.Channel.GetMessagesAsync(amountOfMessagesToDelete + 1).FlattenAsync());

            var embed = new EmbedBuilder();
            embed.WithTitle("Purge");
            embed.WithColor(255, 0, 0);
            embed.WithDescription("Purged " + amountOfMessagesToDelete + " messages.");
            embed.WithCurrentTimestamp();
            embed.WithThumbnailUrl(Context.Guild.IconUrl);
            embed.WithFooter("Built by Bay#6969", null);

            await Context.Channel.SendMessageAsync("", false, embed.Build());
        }



        // --------------------------------MODERATION COMMANDS----------------------------------------


        // --------------------------------ANCIENT COMMANDS----------------------------------------


        [Command("rblacklist", RunMode = RunMode.Async)]
        [RequireUserPermission(GuildPermission.Administrator)]
        public async Task ReportBlacklist(string user)
        {


            SocketUser target = null;
            var mentionedUser = Context.Message.MentionedUsers.FirstOrDefault();
            target = mentionedUser ?? Context.User;

            var account = UserAccounts.GetAccount(target);

            account.ReportBlacklist = "Yes";

            var embed = new EmbedBuilder();
            embed.WithTitle("Blacklisted");
            embed.WithDescription(target.Username + " has been blacklisted from using report.");
            embed.WithThumbnailUrl(target.GetAvatarUrl(ImageFormat.Auto));
            embed.WithColor(new Color(0, 255, 0));

            await Context.Channel.SendMessageAsync("", false, embed.Build());


        }

        [Command("unrblacklist", RunMode = RunMode.Async)]
        [RequireUserPermission(GuildPermission.Administrator)]
        public async Task UnReportBlacklist([Remainder]string user)
        {

            SocketUser target = null;
            var mentionedUser = Context.Message.MentionedUsers.FirstOrDefault();
            target = mentionedUser ?? Context.User;

            var account = UserAccounts.GetAccount(target);

            account.ReportBlacklist = "No";

            var embed = new EmbedBuilder();
            embed.WithTitle("Unblacklisted");
            embed.WithDescription(target.Username + " has been unblacklisted from using report.");
            embed.WithThumbnailUrl(target.GetAvatarUrl(ImageFormat.Auto));
            embed.WithColor(new Color(0, 255, 0));

            await Context.Channel.SendMessageAsync("", false, embed.Build());


        }

        [Command("report", RunMode = RunMode.Async)]
        public async Task Report([Remainder]string reason = null)
        {
            var user = Context.User;

            var account = UserAccounts.GetAccount(Context.User);
            var s = Context.Message.DeleteAsync();

            if (account.ReportBlacklist == "Yes")
            {
                var embed2 = new EmbedBuilder();
                embed2.WithTitle("Blacklisted");
                embed2.WithColor(255, 0, 0);
                embed2.WithDescription("You are blacklisted from using this command.");
                embed2.WithCurrentTimestamp();
                embed2.WithThumbnailUrl(Context.Guild.IconUrl);
                embed2.WithFooter("Built by Bay#6969", null);

                await Context.Channel.SendMessageAsync("", false, embed2.Build());
            }

            else if (reason == null)
            {
                var embed2 = new EmbedBuilder();
                embed2.WithTitle("Report");
                embed2.WithColor(255, 0, 0);
                embed2.WithDescription("To report a user, use the command followed by the report.");
                embed2.WithCurrentTimestamp();
                embed2.WithThumbnailUrl(Context.Guild.IconUrl);
                embed2.WithFooter("Built by Bay#6969", null);

                await Context.Channel.SendMessageAsync("", false, embed2.Build());
            }

            else if (reason != null)
            {
                var embed2 = new EmbedBuilder();
                embed2.WithTitle("Report");
                embed2.WithColor(255, 0, 0);
                embed2.WithDescription("Your report has been logged.");
                embed2.WithCurrentTimestamp();
                embed2.WithThumbnailUrl(Context.Guild.IconUrl);
                embed2.WithFooter("Built by Bay#6969", null);

                await Context.Channel.SendMessageAsync("", false, embed2.Build());


                var embed3 = new EmbedBuilder();
                embed3.WithTitle("Report");
                embed3.WithColor(255, 0, 0);
                embed3.AddField("Report by:", user);
                embed3.AddField("User ID:", user.Id);
                embed3.AddField("Report:", reason);
                embed3.WithCurrentTimestamp();
                embed3.WithThumbnailUrl(Context.Guild.IconUrl);
                embed3.WithFooter("Built by Bay#6969", null);


                await Context.Guild.GetTextChannel(682788066275885082).SendMessageAsync("", false, embed3.Build());
            }
        }

        [Command("logpr")]
        public async Task log(string winner = null, string loser = null, string victorious = null, string losing = null, [Remainder]string score = null)
        {

            var account = UserAccounts.GetAccount(Context.User);

            var user = Context.User as SocketGuildUser;
            var role = (user as IGuildUser).Guild.Roles.FirstOrDefault(x => x.Name == "Statesman");
            var role2 = (user as IGuildUser).Guild.Roles.FirstOrDefault(x => x.Name == "Minor Sovereign");
            var role3 = (user as IGuildUser).Guild.Roles.FirstOrDefault(x => x.Name == "Major Sovereign");


            if (user.Roles.Contains(role))
            {
                if (score != null)
                {
                    var embed2 = new EmbedBuilder();
                    embed2.WithTitle("PR Log");
                    embed2.WithColor(255, 0, 0);
                    embed2.WithDescription("PR logged successfully.");
                    embed2.WithCurrentTimestamp();
                    embed2.WithThumbnailUrl(Context.Guild.IconUrl);
                    embed2.WithFooter("Built by Bay#6969", null);

                    await Context.Channel.SendMessageAsync("", false, embed2.Build());


                    var embed3 = new EmbedBuilder();
                    embed3.WithTitle("PR Log");
                    embed3.WithColor(255, 0, 0);
                    embed3.AddField("Winner:", winner);
                    embed3.AddField("Loser:", loser);
                    embed3.AddField("Victorious Commander:", victorious);
                    embed3.AddField("Losing Commander:", losing);
                    embed3.AddField("Score:", score);
                    embed3.AddField("Posted by:", user + " (" + user.Id + ")");
                    embed3.WithCurrentTimestamp();
                    embed3.WithThumbnailUrl(Context.Guild.IconUrl);
                    embed3.WithFooter("Built by Bay#6969", null);


                    await Context.Guild.GetTextChannel(536266080235290646).SendMessageAsync("", false, embed3.Build());

                }

                else
                {
                    var embed = new EmbedBuilder();
                    embed.WithTitle("PR Log Help");
                    embed.WithColor(255, 0, 0);
                    embed.WithDescription("To log a PR, provide the following arguments in order of listed.");
                    embed.AddField("PR Winner", "Winning Faction");
                    embed.AddField("PR Loser", "Losing Faction");
                    embed.AddField("Victorious Commander", "Winning Commander");
                    embed.AddField("Losing Commander", "Losing Commander");
                    embed.AddField("Score", "PR Score");
                    embed.AddField("Example", "$logpr Lakedaimon Huns WinningCommander123 LosingCommander123 3-0");
                    embed.WithCurrentTimestamp();
                    embed.WithThumbnailUrl(Context.Guild.IconUrl);
                    embed.WithFooter("Built by Bay#6969", null);

                    await Context.Channel.SendMessageAsync("", false, embed.Build());
                }


            }

            else if (user.Roles.Contains(role2))
            {
                if (score != null)
                {
                    var embed2 = new EmbedBuilder();
                    embed2.WithTitle("PR Log");
                    embed2.WithColor(255, 0, 0);
                    embed2.WithDescription("PR logged successfully.");
                    embed2.WithCurrentTimestamp();
                    embed2.WithThumbnailUrl(Context.Guild.IconUrl);
                    embed2.WithFooter("Built by Bay#6969", null);

                    await Context.Channel.SendMessageAsync("", false, embed2.Build());


                    var embed3 = new EmbedBuilder();
                    embed3.WithTitle("PR Log");
                    embed3.WithColor(255, 0, 0);
                    embed3.AddField("Winner:", winner);
                    embed3.AddField("Loser:", loser);
                    embed3.AddField("Victorious Commander:", victorious);
                    embed3.AddField("Losing Commander:", losing);
                    embed3.AddField("Score:", score);
                    embed3.AddField("Posted by:", user + " (" + user.Id + ")");

                    embed3.WithCurrentTimestamp();
                    embed3.WithThumbnailUrl(Context.Guild.IconUrl);
                    embed3.WithFooter("Built by Bay#6969", null);


                    await Context.Guild.GetTextChannel(536266080235290646).SendMessageAsync("", false, embed3.Build());

                }

                else
                {
                    var embed = new EmbedBuilder();
                    embed.WithTitle("PR Log Help");
                    embed.WithColor(255, 0, 0);
                    embed.WithDescription("To log a PR, provide the following arguments in order of listed.");
                    embed.AddField("PR Winner", "Winning Faction");
                    embed.AddField("PR Loser", "Losing Faction");
                    embed.AddField("Victorious Commander", "Winning Commander");
                    embed.AddField("Losing Commander", "Losing Commander");
                    embed.AddField("Score", "PR Score");
                    embed.AddField("Example", "$logpr Lakedaimon Huns WinningCommander123 LosingCommander123 3-0");
                    embed.WithCurrentTimestamp();
                    embed.WithThumbnailUrl(Context.Guild.IconUrl);
                    embed.WithFooter("Built by Bay#6969", null);

                    await Context.Channel.SendMessageAsync("", false, embed.Build());
                }
            }

            else if (user.Roles.Contains(role3))
            {
                if (score != null)
                {
                    var embed2 = new EmbedBuilder();
                    embed2.WithTitle("PR Log");
                    embed2.WithColor(255, 0, 0);
                    embed2.WithDescription("PR logged successfully.");
                    embed2.WithCurrentTimestamp();
                    embed2.WithThumbnailUrl(Context.Guild.IconUrl);
                    embed2.WithFooter("Built by Bay#6969", null);

                    await Context.Channel.SendMessageAsync("", false, embed2.Build());


                    var embed3 = new EmbedBuilder();
                    embed3.WithTitle("PR Log");
                    embed3.WithColor(255, 0, 0);
                    embed3.AddField("Winner:", winner);
                    embed3.AddField("Loser:", loser);
                    embed3.AddField("Victorious Commander:", victorious);
                    embed3.AddField("Losing Commander:", losing);
                    embed3.AddField("Score:", score);
                    embed3.AddField("Posted by:", user + " (" + user.Id + ")");

                    embed3.WithCurrentTimestamp();
                    embed3.WithThumbnailUrl(Context.Guild.IconUrl);
                    embed3.WithFooter("Built by Bay#6969", null);


                    await Context.Guild.GetTextChannel(536266080235290646).SendMessageAsync("", false, embed3.Build());

                }

                else
                {
                    var embed = new EmbedBuilder();
                    embed.WithTitle("PR Log Help");
                    embed.WithColor(255, 0, 0);
                    embed.WithDescription("To log a PR, provide the following arguments in order of listed.");
                    embed.AddField("PR Winner", "Winning Faction");
                    embed.AddField("PR Loser", "Losing Faction");
                    embed.AddField("Victorious Commander", "Winning Commander");
                    embed.AddField("Losing Commander", "Losing Commander");
                    embed.AddField("Score", "PR Score");
                    embed.AddField("Example", "$logpr Lakedaimon Huns WinningCommander123 LosingCommander123 3-0");
                    embed.WithCurrentTimestamp();
                    embed.WithThumbnailUrl(Context.Guild.IconUrl);
                    embed.WithFooter("Built by Bay#6969", null);

                    await Context.Channel.SendMessageAsync("", false, embed.Build());
                }
            }

            else
            {
                var embed = new EmbedBuilder();
                embed.WithTitle("PR Log Failed");
                embed.WithColor(255, 0, 0);
                embed.WithDescription("You must be a Statesman, Minor Sovereign, or Major Sovereign to log a PR.");
                embed.WithCurrentTimestamp();
                embed.WithThumbnailUrl(Context.Guild.IconUrl);
                embed.WithFooter("Built by Bay#6969", null);

                await Context.Channel.SendMessageAsync("", false, embed.Build());
            }

        }


        [Command("addexploiter", RunMode = RunMode.Async)]
        [RequireUserPermission(GuildPermission.Administrator)]
        public async Task exploiteradd(string discord = null, string roblox = null, string link = null, [Remainder]string proof = null)

        {

            if (link != null)
            {
                var embed2 = new EmbedBuilder();
                embed2.WithTitle("Exploiter Logger");
                embed2.WithColor(255, 0, 0);
                embed2.WithDescription("Exploiter logged successfully.");
                embed2.WithCurrentTimestamp();
                embed2.WithThumbnailUrl(Context.Guild.IconUrl);
                embed2.WithFooter("Built by Bay#6969", null);

                await Context.Channel.SendMessageAsync("", false, embed2.Build());


                var embed3 = new EmbedBuilder();
                embed3.WithTitle("Exploiter " + roblox);
                embed3.WithColor(255, 0, 0);
                embed3.AddField("Discord User:", discord);
                embed3.AddField("Roblox User:", roblox);
                embed3.AddField("Profile Link:", link);
                embed3.AddField("Proof:", proof);
                embed3.AddField("Posted by:", Context.User + " (" + Context.User.Id + ")");
                embed3.WithCurrentTimestamp();
                embed3.WithThumbnailUrl(Context.Guild.IconUrl);
                embed3.WithFooter("Built by Bay#6969", null);


                await Context.Guild.GetTextChannel(682786611758759936).SendMessageAsync("", false, embed3.Build());
            }

            else
            {
                var embed = new EmbedBuilder();
                embed.WithTitle("Exploiter Logger Help");
                embed.WithColor(255, 0, 0);
                embed.WithDescription("To log an exploiter, provide the following arguments in order of listed.");
                embed.AddField("Discord Username", "Provide their discord username, either through an @mention or by typing it.");
                embed.AddField("Roblox Username", "Provide the username of their Roblox account.");
                embed.AddField("Roblox Profile Link", "Provide a link to their profile.");
                embed.AddField("Proof", "Provide a link of proof that they exploited.");
                embed.AddField("Example", "$addexploiter sahil#5888 AnaxandridesII https://www.roblox.com/users/122353921/profile https://gyazo.com/0a655eb49d2823c6e9f2705987674edc");
                embed.WithCurrentTimestamp();
                embed.WithThumbnailUrl(Context.Guild.IconUrl);
                embed.WithFooter("Built by Bay#6969", null);

                await Context.Channel.SendMessageAsync("", false, embed.Build());
            }



        }

        [Command("affiliate", RunMode = RunMode.Async)]
        public async Task affiliate(string group = null, string link = null, string role = null, string statesmen = null, [Remainder]string rally = null)

        {

            if (link != null)
            {
                var embed2 = new EmbedBuilder();
                embed2.WithTitle("Affiliation Request");
                embed2.WithColor(255, 0, 0);
                embed2.WithDescription("Affiliation request has been sent.");
                embed2.WithCurrentTimestamp();
                embed2.WithThumbnailUrl(Context.Guild.IconUrl);
                embed2.WithFooter("Built by Bay#6969", null);

                await Context.Channel.SendMessageAsync("", false, embed2.Build());


                var embed3 = new EmbedBuilder();
                embed3.WithTitle("Affiliation Request");
                embed3.WithColor(255, 0, 0);
                embed3.AddField("Group Name:", group);
                embed3.AddField("Group Link:", link);
                embed3.AddField("Role Requested:", role);
                embed3.AddField("Statesmen:", statesmen);
                embed3.AddField("Rally Picture:", rally);
                embed3.AddField("Posted by:", Context.User + " (" + Context.User.Id + ")");
                embed3.WithCurrentTimestamp();
                embed3.WithThumbnailUrl(Context.Guild.IconUrl);
                embed3.WithFooter("Built by Bay#6969", null);


                await Context.Guild.GetTextChannel(682798477234798792).SendMessageAsync("", false, embed3.Build());
            }

            else
            {
                var embed = new EmbedBuilder();
                embed.WithTitle("Group Affiliation Help");
                embed.WithColor(255, 0, 0);
                embed.WithDescription("To send an affiliation request, provide the following arguments in order of listed.");
                embed.AddField("Group Name", "Provide the name of your group.");
                embed.AddField("Group Link", "Provide a link to your group.");
                embed.AddField("Role Requested (Expanding, Minor, Major)", "Request the role you would like.");
                embed.AddField("Statesmen", "Ping them separated by a comma, NO SPACES.");
                embed.AddField("Rally Picture", "A picture of a rally from your group.");
                embed.AddField("Example", "$affiliate Lakedaimon https://www.roblox.com/groups/2939482/Greek-City-State-of-Lakedaimon#!/about Minor @Bay#6969,@sahil#5888 https://cdn.discordapp.com/attachments/585605727645597721/674008391139721216/lakedaiiii.png");
                embed.WithCurrentTimestamp();
                embed.WithThumbnailUrl(Context.Guild.IconUrl);
                embed.WithFooter("Built by Bay#6969", null);

                await Context.Channel.SendMessageAsync("", false, embed.Build());
            }

        }

        [Command("suggest", RunMode = RunMode.Async)]
        [Alias("suggestion")]
        public async Task Suggest([Remainder]string reason = null)
        {
            var user = Context.User;

            var account = UserAccounts.GetAccount(Context.User);
            var s = Context.Message.DeleteAsync();

            if (account.SuggestBlacklist == "Yes")
            {
                var embed2 = new EmbedBuilder();
                embed2.WithTitle("Blacklisted");
                embed2.WithColor(255, 0, 0);
                embed2.WithDescription("You are blacklisted from using this command.");
                embed2.WithCurrentTimestamp();
                embed2.WithThumbnailUrl(Context.Guild.IconUrl);
                embed2.WithFooter("Built by Bay#6969", null);

                await Context.Channel.SendMessageAsync("", false, embed2.Build());
            }

            else if (reason == null)
            {
                var embed2 = new EmbedBuilder();
                embed2.WithTitle("Suggest");
                embed2.WithColor(255, 0, 0);
                embed2.WithDescription("To write a suggestion, use the command followed by the suggestion.");
                embed2.WithCurrentTimestamp();
                embed2.WithThumbnailUrl(Context.Guild.IconUrl);
                embed2.WithFooter("Built by Bay#6969", null);

                await Context.Channel.SendMessageAsync("", false, embed2.Build());
            }

            else if (reason != null)
            {
                var embed2 = new EmbedBuilder();
                embed2.WithTitle("Suggestion");
                embed2.WithColor(255, 0, 0);
                embed2.WithDescription("Your suggestion has been logged.");
                embed2.WithCurrentTimestamp();
                embed2.WithThumbnailUrl(Context.Guild.IconUrl);
                embed2.WithFooter("Built by Bay#6969", null);

                await Context.Channel.SendMessageAsync("", false, embed2.Build());


                var embed3 = new EmbedBuilder();
                embed3.WithTitle("Suggestion");
                embed3.WithColor(255, 0, 0);
                embed3.AddField("Suggestion by:", user);
                embed3.AddField("User ID:", user.Id);
                embed3.AddField("Suggestion:", reason);
                embed3.WithCurrentTimestamp();
                embed3.WithThumbnailUrl(Context.Guild.IconUrl);
                embed3.WithFooter("Built by Bay#6969", null);



                await Context.Guild.GetTextChannel(682787747441934354).SendMessageAsync("", false, embed3.Build());
            }
        }

        [Command("sblacklist", RunMode = RunMode.Async)]
        [RequireUserPermission(GuildPermission.Administrator)]
        public async Task SuggestBlacklist(string user)
        {


            SocketUser target = null;
            var mentionedUser = Context.Message.MentionedUsers.FirstOrDefault();
            target = mentionedUser ?? Context.User;

            var account = UserAccounts.GetAccount(target);

            account.SuggestBlacklist = "Yes";

            var embed = new EmbedBuilder();
            embed.WithTitle("Blacklisted");
            embed.WithDescription(target.Username + " has been blacklisted from using suggest.");
            embed.WithThumbnailUrl(target.GetAvatarUrl(ImageFormat.Auto));
            embed.WithColor(new Color(0, 255, 0));

            await Context.Channel.SendMessageAsync("", false, embed.Build());


        }

        [Command("unsblacklist", RunMode = RunMode.Async)]
        [RequireUserPermission(GuildPermission.Administrator)]
        public async Task UnSuggestBlacklist([Remainder]string user)
        {


            SocketUser target = null;
            var mentionedUser = Context.Message.MentionedUsers.FirstOrDefault();
            target = mentionedUser ?? Context.User;

            var account = UserAccounts.GetAccount(target);

            account.SuggestBlacklist = "No";

            var embed = new EmbedBuilder();
            embed.WithTitle("Unblacklisted");
            embed.WithDescription(target.Username + " has been unblacklisted from using suggest.");
            embed.WithThumbnailUrl(target.GetAvatarUrl(ImageFormat.Auto));
            embed.WithColor(new Color(0, 255, 0));

            await Context.Channel.SendMessageAsync("", false, embed.Build());


        }



        // --------------------------------ANCIENT COMMANDS----------------------------------------




        // --------------------------------OWNER COMMANDS----------------------------------------

        [Group("shutdown")]
        [RequireOwner]
        public class shutdown : ModuleBase<SocketCommandContext>
        {
            [Command]
            public async Task stop()
            {
                var user = Context.User;

                var embed = new EmbedBuilder();
                embed.WithTitle("Shutting Down..");
                embed.WithColor(255, 0, 0);
                embed.WithDescription("Shutdown by: " + user);
                embed.WithCurrentTimestamp();
                embed.WithThumbnailUrl(user.GetAvatarUrl(ImageFormat.Auto));
                embed.WithFooter("Built by Bay#6969", null);

                await Context.Channel.SendMessageAsync("", false, embed.Build());

                Console.WriteLine($"{DateTime.Now}: Shutting down...");

                System.Threading.Thread.Sleep(1500);


                System.Environment.Exit(0);
            }
        }


        [Command("SetGame")]
        [Summary("Sets a 'Game'for the bot :video_game: (Only Developers can use this command.)")]
        [RequireOwner]
        public async Task setgame([Remainder]string game)
        {
            await (Context.Client as DiscordSocketClient).SetGameAsync(game);
            await Context.Channel.SendMessageAsync($"Successfully set the game to '**{game}**'");
            Console.WriteLine($"{DateTime.Now}: Game was changed to {game}");

        }

        [Command("data")]
        [RequireOwner()]
        public async Task getData()
        {


            var user = Context.User;

            var embed = new EmbedBuilder();
            embed.WithTitle("Bot Data");
            embed.WithColor(255, 0, 0);
            embed.AddField("Pairs", DataStore.GetPairsCount());
            embed.WithCurrentTimestamp();
            embed.WithThumbnailUrl(Context.Client.CurrentUser.GetAvatarUrl(ImageFormat.Auto));
            embed.WithFooter("Built by Bay#6969", null);

            DataStore.AddPairToStorage("Count" + DataStore.GetPairsCount(), "TheCount" + DataStore.GetPairsCount());
            await Context.Channel.SendMessageAsync("", false, embed.Build());

            Console.WriteLine($"{DateTime.Now}: Data has " + DataStore.GetPairsCount() + " pairs.");

        }


        [Command("cleardata")]
        [RequireOwner()]
        public async Task clearData()
        {
            await Context.Channel.SendMessageAsync("Data has been wiped.");

            string[] filePaths = Directory.GetFiles(@"X:\source\repos\Ancient Bot\Ancient Bot\bin\Debug\Resources", "*accounts.json");
            foreach (string filePath in filePaths)
            {
                File.Delete(filePath);
            }

            Console.WriteLine($"{DateTime.Now}: Clearing data...");

            System.Threading.Thread.Sleep(1500);

            System.Diagnostics.Process.Start(Environment.GetCommandLineArgs()[0], Environment.GetCommandLineArgs().Length > 1 ? string.Join(" ", Environment.GetCommandLineArgs().Skip(1)) : null);

            System.Environment.Exit(0);

        }


        [Group("restart")]
        [RequireOwner]
        public class start : ModuleBase<SocketCommandContext>
        {
            [Command]
            public async Task startup()
            {
                var user = Context.User;

                var embed = new EmbedBuilder();
                embed.WithTitle("Starting Up..");
                embed.WithColor(255, 0, 0);
                embed.WithDescription("Started by: " + user);
                embed.WithCurrentTimestamp();
                embed.WithThumbnailUrl(user.GetAvatarUrl(ImageFormat.Auto));
                embed.WithFooter("Built by Bay#6969", null);

                await Context.Channel.SendMessageAsync("", false, embed.Build());

                Console.WriteLine($"{DateTime.Now}: Restarting...");

                System.Threading.Thread.Sleep(1500);

                System.Diagnostics.Process.Start(Environment.GetCommandLineArgs()[0], Environment.GetCommandLineArgs().Length > 1 ? string.Join(" ", Environment.GetCommandLineArgs().Skip(1)) : null);

                System.Environment.Exit(0);
            }

        }

        [Command("annoy", RunMode = RunMode.Async)]
        [RequireOwner()]
        public async Task annoying(IGuildUser user, [Remainder]string reason)
        {
            var userAccount = UserAccounts.GetAccount((SocketUser)user);
            var dmChannel = await user.GetOrCreateDMChannelAsync();


            var embed = new EmbedBuilder();
            embed.WithTitle("Beginning..");
            embed.WithColor(255, 0, 0);
            embed.WithDescription("Annoying...");
            embed.WithCurrentTimestamp();
            embed.WithThumbnailUrl(user.GetAvatarUrl(ImageFormat.Auto));
            embed.WithFooter("Built by Bay#6969", null);

            await Context.Channel.SendMessageAsync("", false, embed.Build());


            await dmChannel.SendMessageAsync(reason);
            System.Threading.Thread.Sleep(1500);
            await dmChannel.SendMessageAsync(reason);
            System.Threading.Thread.Sleep(1500);
            await dmChannel.SendMessageAsync(reason);
            System.Threading.Thread.Sleep(1500);
            await dmChannel.SendMessageAsync(reason);
            System.Threading.Thread.Sleep(1500);
            await dmChannel.SendMessageAsync(reason);
            System.Threading.Thread.Sleep(1500);
            await dmChannel.SendMessageAsync(reason);
            System.Threading.Thread.Sleep(1500);
            await dmChannel.SendMessageAsync(reason);
            System.Threading.Thread.Sleep(1500);
            await dmChannel.SendMessageAsync(reason);
            System.Threading.Thread.Sleep(1500);
            await dmChannel.SendMessageAsync(reason);
            System.Threading.Thread.Sleep(1500);
            await dmChannel.SendMessageAsync(reason);
            System.Threading.Thread.Sleep(1500);
            await dmChannel.SendMessageAsync(reason);
            System.Threading.Thread.Sleep(1500);
            await dmChannel.SendMessageAsync(reason);
            System.Threading.Thread.Sleep(1500);
            await dmChannel.SendMessageAsync(reason);
            System.Threading.Thread.Sleep(1500);
            await dmChannel.SendMessageAsync(reason);
            System.Threading.Thread.Sleep(1500);

            var embed2 = new EmbedBuilder();
            embed2.WithTitle("Completed");
            embed2.WithColor(255, 0, 0);
            embed2.WithDescription("User has been annoyed.");
            embed2.WithCurrentTimestamp();
            embed2.WithThumbnailUrl(user.GetAvatarUrl(ImageFormat.Auto));
            embed2.WithFooter("Built by Bay#6969", null);

            await Context.Channel.SendMessageAsync("", false, embed2.Build());

            Console.WriteLine($"{DateTime.Now}: Annoy command has been used on " + user.Username + ".");

        }

        [Command("setavatar")]
        [RequireOwner()]
        public async Task SetAvatar(string link)
        {
            var s = Context.Message.DeleteAsync();

            try
            {
                var webClient = new WebClient();
                byte[] imageBytes = webClient.DownloadData(link);

                var stream = new MemoryStream(imageBytes);

                var image = new Image(stream);
                await Context.Client.CurrentUser.ModifyAsync(k => k.Avatar = image);


                var embed = new EmbedBuilder();
                embed.WithTitle("Profile Picture Set");
                embed.WithColor(0, 255, 0);
                embed.WithDescription("Profile picture has been set.");
                embed.WithCurrentTimestamp();
                embed.WithFooter("Built by Bay#6969", null);

                await Context.Channel.SendMessageAsync("", false, embed.Build());

                Console.WriteLine($"{DateTime.Now}: Profile picture has been set successfully.");

            }
            catch (Exception)
            {

                var embed = new EmbedBuilder();
                embed.WithTitle("Error");
                embed.WithColor(255, 0, 0);
                embed.WithDescription("Profile picture could not be set.");
                embed.WithCurrentTimestamp();
                embed.WithFooter("Built by Bay#6969", null);

                await Context.Channel.SendMessageAsync("", false, embed.Build());

                Console.WriteLine($"{DateTime.Now}: Profile picture has failed to set.");
            }
        }

        [Command("blacklist")]
        [RequireOwner()]
        public async Task blacklist(IGuildUser user)
        {

            SocketUser target = null;
            var mentionedUser = Context.Message.MentionedUsers.FirstOrDefault();
            target = mentionedUser ?? Context.User;

            var account = UserAccounts.GetAccount(target);

            account.Blacklisted = "Yes";

            var embed = new EmbedBuilder();
            embed.WithTitle("Blacklisted");
            embed.WithDescription(target.Username + " has been blacklisted from using commands.");
            embed.WithThumbnailUrl(user.GetAvatarUrl(ImageFormat.Auto));
            embed.WithColor(new Color(0, 255, 0));

            await Context.Channel.SendMessageAsync("", false, embed.Build());

            Console.WriteLine($"{DateTime.Now}: " + target.Username + " has been blacklisted from using commands.");
        }

        [Command("unblacklist")]
        [RequireOwner()]
        public async Task unblacklist(IGuildUser user)
        {

            SocketUser target = null;
            var mentionedUser = Context.Message.MentionedUsers.FirstOrDefault();
            target = mentionedUser ?? Context.User;

            var account = UserAccounts.GetAccount(target);

            account.Blacklisted = "No";

            var embed = new EmbedBuilder();
            embed.WithTitle("Blacklist Revoked");
            embed.WithDescription(target.Username + " has been unblacklisted from using commands.");
            embed.WithThumbnailUrl(user.GetAvatarUrl(ImageFormat.Auto));
            embed.WithColor(new Color(0, 255, 0));

            await Context.Channel.SendMessageAsync("", false, embed.Build());

            Console.WriteLine($"{DateTime.Now}: " + target.Username + " has been unblacklisted from using commands.");
        }

        // --------------------------------OWNER COMMANDS----------------------------------------
    }
}

