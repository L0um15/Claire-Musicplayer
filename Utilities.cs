using System;
using System.Collections.Generic;
using System.Net;
using System.Reflection;
using System.Text;

namespace Claire_Musicplayer
{
    public static class Utilities
    {
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
    }
}
