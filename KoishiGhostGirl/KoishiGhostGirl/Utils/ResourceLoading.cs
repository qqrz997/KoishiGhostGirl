using System.IO;
using System.Reflection;

namespace KoishiGhostGirl.Utils
{
    internal class ResourceLoading
    {
        /// <summary>
        /// Loads a file from resources into memory
        /// </summary>
        public static byte[] LoadFromResource(string resourcePath)
        {
            return GetResource(Assembly.GetCallingAssembly(), resourcePath);
        }

        private static byte[] GetResource(Assembly assembly, string resourcePath)
        {
            Stream stream = assembly.GetManifestResourceStream(resourcePath);
            byte[] data = new byte[stream.Length];
            stream.Read(data, 0, (int)stream.Length);
            return data;
        }
    }
}
