using HarmonyLib;
using Vintagestory.API.Client;
using Vintagestory.API.Common;
using Vintagestory.API.Config;
using Vintagestory.API.Server;

namespace RPGDifficultyPatchesNS
{
    [HarmonyPatch] // Place on any class with harmony patches
    public class RPGDifficultyPatchesNSModSystem : ModSystem
    {

        public override void Start(ICoreAPI api) {
            if (api.Side == EnumAppSide.Server) {
                //Main.clientApi = api as ICoreClientAPI;

                if (!Harmony.HasAnyPatches(Mod.Info.ModID)) {
                    Main.harmonyRef = new Harmony(Mod.Info.ModID);
                    Main.harmonyRef.PatchAll(); // Applies all harmony patches
                }
                Main.Init(api as ICoreServerAPI);
                Config.LoadSettings();
            }
        }

        public override void StartServerSide(ICoreServerAPI api) {

        }

        public override void StartClientSide(ICoreClientAPI api) {

        }

        public override void Dispose()
        {
            Main.harmonyRef?.UnpatchAll(Mod.Info.ModID);
        }

    }
}
