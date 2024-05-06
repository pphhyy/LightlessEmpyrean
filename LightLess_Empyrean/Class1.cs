using RimWorld;
using RimWorld.Planet;
using HarmonyLib;
using Verse;

namespace LightLess_Empyrean
{
    public class BiomeWorker_LightlessEmpyrean : BiomeWorker
    {
        public override float GetScore(Tile tile, int tileID)
        {
            if (tile.WaterCovered)
            {
                return -100f;
            }
            if (tile.temperature < -15f || tile.temperature > 20f)
            {
                return 0f;
            }
            if (tile.rainfall < 1300 || tile.rainfall > 1500)
            {
                return 0f;
            }
            return 15f + (tile.temperature - 6f) + (tile.rainfall - 570) / 180f;
        }
    }



    [StaticConstructorOnStartup]
    public class Main
    {
        static Main()
        {
            Log.Message("1");
            Harmony harmony = new Harmony("com.pphhyy_LightLessEmpyrean_Gravel");
            harmony.PatchAll();
            Log.Message("2");
        }

    }

    [HarmonyPatch(typeof(GenStep_Terrain))]
    [HarmonyPatch("TerrainFrom")]
    public static class GenStep_Terrain_TerrainFrom_Patch
    {
        [HarmonyPostfix]
        public static void ReplaceTerrainWith(Map map, ref TerrainDef __result)
        {
            Log.Message("3");
            switch (map.Biome.defName)
            {

                case "pphhyy_LightlessEmpyrean_Biome":
                    Log.Message("4");
                    if (__result == TerrainDefOf.Gravel)
                    {
                        __result = TerrainDef.Named("pphhyy_DarkGravel");
                    }
                    return;
                default:
                    return;
            }
        }
    }

}