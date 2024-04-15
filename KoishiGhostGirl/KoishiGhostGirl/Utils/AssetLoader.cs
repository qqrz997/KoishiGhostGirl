using UnityEngine;
using Object = UnityEngine.Object;

namespace KoishiGhostGirl.Utils
{
    internal class AssetLoader
    {
        private readonly string bundleResourcePath;

        public AssetLoader(string bundleResourcePath)
        {
            this.bundleResourcePath = bundleResourcePath;
            LoadBundle();
        }

        private AssetBundle bundle = null!;

        private void LoadBundle()
        {
            bundle = AssetBundle.LoadFromMemory(ResourceLoading.LoadFromResource(bundleResourcePath));
            if (bundle != null)
            {
                PluginLog.Debug($"Successfully loaded assetbundle from {bundleResourcePath}");
            }
            else
            {
                PluginLog.Error($"Failed to load assetbundle from {bundleResourcePath}");
            }
        }

        public Object GetAssetFromBundle<T>(string assetPath)
        {
            Object? asset = bundle?.LoadAsset<Object>(assetPath);
            if (asset != null)
            {
                PluginLog.Debug($"loaded {typeof(T)} from bundle: {assetPath}");
                return asset;
            }
            else
            {
                PluginLog.Error($"couldn't load {typeof(T)} from bundle: {assetPath}");
                return null!;
            }
        }

        public void Unload()
        {
            bundle?.Unload(false);
        }
    }
}
