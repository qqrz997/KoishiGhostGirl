using KoishiGhostGirl.Utils;
using System;
using UnityEngine;

namespace KoishiGhostGirl
{
    internal class Koishi
    {
        public static GameObject Mesh { get; private set; } = null!;
        public static AudioClip HeartbeatAudio { get; private set; } = null!;
        public static Material OriginalGirlMaterial { get; private set; } = null!;

        public static bool Inited = false;

        public Koishi() 
        {
            Init();
        } 

        private void Init()
        {
            try
            {
                // Load koishi from embedded asset bundle
                var assetLoader = new AssetLoader("KoishiGhostGirl.Resources.koishi");

                Mesh = (GameObject)assetLoader.GetAssetFromBundle<GameObject>("Assets/Bundles/Koishi2/koishi.prefab");
                HeartbeatAudio = (AudioClip)assetLoader.GetAssetFromBundle<AudioClip>("Assets/Bundles/Koishi2/heartbeatMusic.wav");
                OriginalGirlMaterial = (Material)assetLoader.GetAssetFromBundle<Material>("Assets/Bundles/Koishi2/Materials/koishiUnlit.mat");

                assetLoader.Unload();

                Inited = true;
            }
            catch (Exception ex)
            {
                PluginLog.Error("Problem encountered when initializing koishi");
                PluginLog.Error(ex.ToString());
            }
        }
    }
}
