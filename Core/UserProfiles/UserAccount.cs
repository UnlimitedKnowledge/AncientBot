using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AncientBot.Core.UserProfiles
{
    public class UserAccount
    {
        public ulong ID { get; set; }

        public string User { get; set; }

        public string Nick { get; set; }

        public uint XP { get; set; }

        public uint LevelNumber
        {
            get
            {
                return (uint)Math.Sqrt(XP / 50);
            }

        }

        public bool IsMuted { get; set; }

        public uint NumberOfWarnings { get; set; }

        public string Reason { get; set; }

        public string Reason2 { get; set; }

        public string Reason3 { get; set; }

        public string Newest { get; set; }

        public uint NumberOfStrikes { get; set; }

        public string StrikeReason { get; set; }

        public string Strike { get; set; }

        public string Strike2 { get; set; }

        public string Strike3 { get; set; }

        public string NewestS { get; set; }

        public string Blacklisted { get; set; }

        public string ReportBlacklist { get; set; }

        public string SuggestBlacklist { get; set; }
    }
}
