using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Claire_Musicplayer.Services
{
    public static class DirectoryHelper
    {
        public static string CurrentDirectory { get; private set; } = Directory.GetCurrentDirectory();
        public readonly static List<string> Tracklist = new List<string>();
        /// <summary>
        /// Will attempt to change directory and handle DirectoryNotFoundException
        /// </summary>
        /// <param name="path">Can be absolute or relative path</param>
        public static bool TrySetCurrentDirectory(string path)
        {
            try
            {
                if (Path.IsPathRooted(path))
                {
                    Directory.SetCurrentDirectory(path);
                    CurrentDirectory = Directory.GetCurrentDirectory();
                    return true;
                }
                else
                {
                    Directory.SetCurrentDirectory(Path.Combine(CurrentDirectory, path));
                    CurrentDirectory = Directory.GetCurrentDirectory();
                    return true;
                }
            }
            catch (DirectoryNotFoundException)
            {
                MessageExtensions.WriteLine($"Directory: \"{path}\" not found");
            }
            return false;
        }

        public static void PrepareTracklist()
        {
            Tracklist.Clear();
            string[] allFiles = Directory.GetFiles(CurrentDirectory, "*", SearchOption.TopDirectoryOnly);
            for (int i = 0; i < allFiles.Length; i++)
            {
                if (Utilities.AllowedMediaExtensions.Contains(Path.GetExtension(allFiles[i]).ToLower()))
                {
                    Tracklist.Add(allFiles[i]);
                }
            }
        }

    }
}
