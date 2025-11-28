using HarmonyLib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vintagestory.API.Client;
using Vintagestory.API.Config;
using Vintagestory.API.Server;
using Vintagestory.Client.NoObf;

namespace RPGDifficultyPatchesNS.Patches
{
    public static class IncreaseStatsByDistancePadding
    {
        static int _increaseStatsEveryDistance_Backup = 0;

        public static void PatchDistance() {
            _increaseStatsEveryDistance_Backup = RPGDifficulty.Configuration.increaseStatsEveryDistance;
            RPGDifficulty.Configuration.increaseStatsEveryDistance += Config.settings.increaseStatsByDistancePadding;
        }

        public static void RestoreOriginalDistance() {
            RPGDifficulty.Configuration.increaseStatsEveryDistance = _increaseStatsEveryDistance_Backup;
        }
    }
}
