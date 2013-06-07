using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleInjector;

namespace Trax.Leaderboard
{
    internal static class LeaderboardContainer
    {
        public static Container Container { get; private set; }

        public static void SetupNewContainer()
        {
            var container = new Container();
            container.RegisterSingle<ILeaderboardData, LeaderboardData>();
            container.Verify();
            Container = container;
        }
    }
}
