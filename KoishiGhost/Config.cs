using BepInEx.Configuration;

namespace KoishiGhost
{
    public class KoishiConfig
    {
        public static ConfigEntry<bool> useOriginalShader;
        public static ConfigEntry<bool> useOriginalSound;

        public static void ConfigSettings()
        {
            useOriginalShader = Plugin.Instance.Config.Bind("Shaders", "Use Original Shaders", false,
                "Enable this to make Koishi use the original lit shaders to give her the same \"glow-in-the-dark\" appearance as the Ghost Girl.");
            useOriginalSound = Plugin.Instance.Config.Bind("Audio", "Use Original Audio", false,
                "Enable this to make Koishi use the original sound effects of the Ghost Girl");
        }
    }
}