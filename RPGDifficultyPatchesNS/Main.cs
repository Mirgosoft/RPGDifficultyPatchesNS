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

namespace RPGDifficultyPatchesNS
{
    public static class Main
    {
        public static Harmony harmonyRef = null;
        public static ICoreServerAPI serverApi = null;

        public static void Init(ICoreServerAPI api) {
            serverApi = api;
        }

    }
}
