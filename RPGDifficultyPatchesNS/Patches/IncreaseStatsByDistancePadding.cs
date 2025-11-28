using HarmonyLib;
using RPGDifficulty;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vintagestory.API.Client;
using Vintagestory.API.Common.Entities;
using Vintagestory.API.Config;
using Vintagestory.API.Server;
using Vintagestory.Client.NoObf;

namespace RPGDifficultyPatchesNS.Patches
{
    public static class IncreaseStatsByDistancePadding
    {
        static int _increaseStatsEveryDistance_Backup = -999;

        public static void PatchDistance(Entity entity) {
            double diff_pos_X = entity.Pos.X - Initialization.DefaultSpawnPosition.X;
            double diff_pos_Z = entity.Pos.Z - Initialization.DefaultSpawnPosition.Z;

            if (diff_pos_X < 0d) // Более быстрый аналог Math.abs();
                diff_pos_X *= -1d;
            if (diff_pos_Z < 0d) // Более быстрый аналог Math.abs();
                diff_pos_Z *= -1d;

            double distance = diff_pos_X + diff_pos_Z; // (да-да, именно так оптимизированно рассчитывает дистанцию RPGDifficulty.)

            // Рассчитываем, какой должен быть параметр  .increaseStatsEveryDistance
            // с учётом нашего отступа Config.settings.increaseStatsByDistancePadding.
            
            // Бекап перед изменениями.
            _increaseStatsEveryDistance_Backup = RPGDifficulty.Configuration.increaseStatsEveryDistance;

            // 1) Если сущность находится в зонах, где необходимо повышение статов.
            double increase_rank = 0;
            if (distance > RPGDifficulty.Configuration.increaseStatsEveryDistance + Config.settings.increaseStatsByDistancePadding) 
            {
                double distance_without_padding = distance - Config.settings.increaseStatsByDistancePadding;
                increase_rank = Math.Floor(distance_without_padding / (double)RPGDifficulty.Configuration.increaseStatsEveryDistance);

                RPGDifficulty.Configuration.increaseStatsEveryDistance = (int)(distance / (increase_rank + 1d)) + 3;
                // Где:
                //   +1d - для пропуска нулевой области не приносит бонусов к статам по задумке самого мода.
                //   +3 - запас по блокам, чтобы сущность точно была внутри нужной области
                return;
            }

            // 2) Если же сущность находится в нулевой зоне, где повышение статов не требуется.
            RPGDifficulty.Configuration.increaseStatsEveryDistance += Config.settings.increaseStatsByDistancePadding;
        }

        public static void RestoreOriginalDistance(Entity entity) {
            if (_increaseStatsEveryDistance_Backup < 0)
                return;
            RPGDifficulty.Configuration.increaseStatsEveryDistance = _increaseStatsEveryDistance_Backup;
            _increaseStatsEveryDistance_Backup = -999;
        }
    }
}
