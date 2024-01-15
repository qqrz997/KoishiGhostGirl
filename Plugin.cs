using UnityEngine;
using HarmonyLib;
using BepInEx;
using BepInEx.Logging;
using KoishiGhost.Patches;

namespace KoishiGhost
{
    [BepInPlugin(modGUID, modName, modVersion)]
    public class Plugin : BaseUnityPlugin
    {
        private const string modGUID = "qqrz.KoishiGhost";
        private const string modName = "Koishi Komeiji Ghost Girl";
        private const string modVersion = "1.0.0";

        private readonly Harmony harmony = new Harmony(modGUID);

        private static Plugin Instance;

        internal static AssetBundle koishBundle;
        public static GameObject koishMesh;
        public static AudioClip koishHeartbeatAudio;

        public static ManualLogSource log;

        void Awake()
        {
            if (Instance != null && Instance != this)
            { Destroy(this); }
            else { Instance = this; }

            log = BepInEx.Logging.Logger.CreateLogSource(modGUID);
            log.LogInfo("Koishi Ghost loaded");

            string dir = Instance.Info.Location.TrimEnd("KoishiGhost.dll".ToCharArray());

            koishBundle = AssetBundle.LoadFromFile(dir + "koishi");
            if (koishBundle != null)
            {
                koishMesh = koishBundle.LoadAsset<GameObject>("Assets/Bundles/Koishi2/koishi.prefab");
                koishHeartbeatAudio = koishBundle.LoadAsset<AudioClip>("Assets/Bundles/Koishi2/heartbeatMusic.wav");
                log.LogInfo("Asset bundle loaded");
            }
            else { log.LogError("Asset bundle not loaded"); }

            harmony.PatchAll(typeof(DressGirlAIPatch));
        }
    }
}
