using BepInEx.Logging;

namespace KoishiGhostGirl
{
    internal class PluginLog
    {
        internal static ManualLogSource Log { get; private set; } = null!;

        public PluginLog(ManualLogSource log)
        {
            Log = log;
        }

        public static void Error(string message)
        {
            Log.LogError(message);
        }

        public static void Info(string message)
        {
            Log.LogInfo(message);
        }

        public static void Debug(string message)
        {
#if DEBUG
            Info(message);
#else
            Log.LogDebug(message);
#endif
        }
    }
}
