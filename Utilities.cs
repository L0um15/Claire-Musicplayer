using System;
using System.Collections.Generic;
using System.Net;
using System.Reflection;
using System.Text;

namespace Claire_Musicplayer
{
    public static class Utilities
    {

        public static string[] AllowedMediaExtensions = {
            ".mp3", ".m4a", ".wav",
            ".aiff", ".alac", ".flac",
            ".midi", ".ogg"
        };

        public static string GetLatestVersion()
        {
            try
            {
                string result = new WebClient().DownloadString("https://raw.githubusercontent.com/L0um15/Claire-Musicplayer/main/version.txt");
                if (result != GetVersion())
                {
                    return result;
                }
            }
            catch (WebException) { }
            return null;
        }

        public static string GetVersion()
            => Assembly.GetExecutingAssembly().GetName().Version.ToString();

        public static bool ContainsAll(this string str, ReadOnlySpan<string> words)
        {
            bool found = true;
            foreach (string item in words)
            {
                if (!str.Contains(item, StringComparison.InvariantCultureIgnoreCase))
                {
                    found = false;
                    break;
                }
            }
            return found;
        }
    }
}
