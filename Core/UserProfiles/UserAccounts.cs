using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord.WebSocket;

namespace AncientBot.Core.UserProfiles
{
    public static class UserAccounts
    {
        private static List<UserAccount> accounts;

        private static string accountsFile = "Resources/accounts.json";

        static UserAccounts()
        {
            if (DataStorage.SaveExists(accountsFile))
            {
                accounts = DataStorage.LoadUserAccounts(accountsFile).ToList();
            }
            else
            {
                accounts = new List<UserAccount>();
                SaveAccounts();
            }
        }

        public static void SaveAccounts()
        {
            DataStorage.SaveUserAccounts(accounts, accountsFile);
        }

        public static UserAccount GetAccount(SocketUser user)
        {
            return GetOrCreateAccount(user.Id);
        }

        private static UserAccount GetOrCreateAccount(ulong id)
        {
            var result = from a in accounts
                         where a.ID == id
                         select a;

            var account = result.FirstOrDefault();
            if (account == null) account = CreateUserAccount(id);
            return account;
        }

        private static UserAccount CreateUserAccount(ulong id)
        {
            var newAccount = new UserAccount()
            {
                ID = id,
                XP = 0,
                Reason = "N/A",
                Reason2 = "N/A",
                Reason3 = "N/A",
                Newest = "N/A",
                Strike = "N/A",
                Strike2 = "N/A",
                Strike3 = "N/A",
                NewestS = "N/A",
                Blacklisted = "N/A",
                ReportBlacklist = "N/A",
                SuggestBlacklist = "N/A"
            };

            accounts.Add(newAccount);
            SaveAccounts();
            return newAccount;
        }
    }
}
