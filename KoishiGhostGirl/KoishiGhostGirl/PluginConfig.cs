using BepInEx.Configuration;

namespace KoishiGhostGirl
{
    internal class PluginConfig
    {
        public static PluginConfig Instance { get; private set; } = null!;

        private ConfigEntry<bool> useOriginalShader;

        private ConfigEntry<bool> useOriginalSound;

        public bool UseOriginalShader
        {
            get => useOriginalShader.Value;
            set => useOriginalShader.Value = value;
        }

        public bool UseOriginalSound
        {
            get => useOriginalSound.Value;
            set => useOriginalSound.Value = value;
        }

        public PluginConfig()
        {
            Instance = this;

            useOriginalShader = Plugin.Instance.Config.Bind("Shaders", "Use Original Shaders", false,
                "Enable this to make Koishi use the original lit shaders to give her the same \"glow-in-the-dark\" appearance as the Ghost Girl.");

            useOriginalSound = Plugin.Instance.Config.Bind("Audio", "Use Original Audio", false,
                "Enable this to make Koishi use the original sound effects of the Ghost Girl");
        }
    }
}
