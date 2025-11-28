using HarmonyLib;
using RPGDifficultyPatchesNS.Patches;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Vintagestory.API.Common;
using Vintagestory.API.Common.Entities;
using Vintagestory.Common;
using Vintagestory.GameContent;

namespace RPGDifficultyPatchesNS
{

    [HarmonyPatch]
    internal static class RPGDifficulty_Initialization__LevelUPOnExperienceIncrease_Patches
    {
        static MethodBase TargetMethod() { // (Для ref или out аргумента)
            return AccessTools.Method(typeof(RPGDifficulty.Initialization), "LevelUPOnExperienceIncrease",
                new Type[] { 
                    typeof(IPlayer), 
                    typeof(string),
                    typeof(ulong).MakeByRefType() // ref или out аргумент
                }
            );
        }
        public static void Prefix(RPGDifficulty.Initialization __instance, IPlayer player,
                                  string type, ulong amount)
        {
            IncreaseStatsByDistancePadding.PatchDistance();
        }
        public static void Postfix(RPGDifficulty.Initialization __instance, IPlayer player,
                                   string type, ulong amount)
        {
            IncreaseStatsByDistancePadding.RestoreOriginalDistance();
        }
    }

    [HarmonyPatch(typeof(RPGDifficulty.Initialization), "SetEntityStats", new[] { typeof(Entity) })]
    internal static class RPGDifficulty_Initialization__SetEntityStats_Patches
    {
        public static void Prefix(RPGDifficulty.Initialization __instance, Entity entity) {
            IncreaseStatsByDistancePadding.PatchDistance();
        }
        public static void Postfix(RPGDifficulty.Initialization __instance, Entity entity) {
            IncreaseStatsByDistancePadding.RestoreOriginalDistance();
        }
    }



}
