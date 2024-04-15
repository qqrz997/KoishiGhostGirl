using BepInEx;
using HarmonyLib;

namespace KoishiGhostGirl
{
    [BepInPlugin(PluginInfo.GUID, PluginInfo.Name, PluginInfo.Version)]
    public class Plugin : BaseUnityPlugin
    {
        public static Plugin Instance { get; private set; } = null!;
        internal static Harmony? Harmony { get; set; }

        private void Awake()
        {
            new PluginLog(base.Logger);
            Instance = this;
            Harmony = new Harmony(PluginInfo.GUID);
            new PluginConfig();

            new Koishi();

            ApplyPatches();

            PluginLog.Info($"{PluginInfo.GUID} v{PluginInfo.Version} has loaded!");
        }

        private void ApplyPatches()
        {
            Harmony?.PatchAll();
            PluginLog.Debug("Finished patching!");
        }
    }
}
