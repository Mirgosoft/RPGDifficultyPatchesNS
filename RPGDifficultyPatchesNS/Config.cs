using Cairo;
using HarmonyLib;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using Vintagestory.API.Client;
using Vintagestory.API.Config;
using Vintagestory.API.Server;
using Vintagestory.Client.NoObf;

namespace RPGDifficultyPatchesNS
{
    public static class Config
    {
        static string _folderPath { get { return GamePaths.ModConfig + "/RPGDifficulty/config"; } }
        static string _filename = "base_ns_patches.json";

        public static ConfigData settings = new ConfigData();



        public static void SaveSettings() {
            if (!Directory.Exists(Config._folderPath))
                Directory.CreateDirectory(Config._folderPath);

            string settings_str = JsonConvert.SerializeObject(settings, Formatting.Indented);
            File.WriteAllText(_folderPath + "/" + _filename, settings_str);
        }

        public static void LoadSettings() {
            if (!File.Exists(_folderPath + "/" + _filename)) {
                SaveSettings();
                return;
            }

            settings = JsonConvert.DeserializeObject<ConfigData>(File.ReadAllText(_folderPath + "/" + _filename));
        }

        public class ConfigData {
            public int increaseStatsByDistancePadding = 0;

        }
    }
}
