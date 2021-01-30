using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Claire_Musicplayer.Services
{
    public static class DirectoryHelper
    {
        public static string CurrentDirectory { get; private set; } = Directory.GetCurrentDirectory();

        /// <summary>
        /// Will attempt to change directory and handle DirectoryNotFoundException
        /// </summary>
        /// <param name="path">Can be absolute or relative path</param>
        public static void TrySetCurrentDirectory(string path)
        {
            try
            {
                if (Path.IsPathRooted(path))
                {
                    Directory.SetCurrentDirectory(path);
                    CurrentDirectory = Directory.GetCurrentDirectory();
                }
                else
                {
                    Directory.SetCurrentDirectory(Path.Combine(CurrentDirectory, path));
                    CurrentDirectory = Directory.GetCurrentDirectory();
                }
            }
            catch (DirectoryNotFoundException)
            {
                MessageExtensions.WriteLine($"Directory: \"{path}\" not found");
            }
        }

        public static bool ContainsAll(this string str, string[] words)
        {
            bool found = true;
            foreach(string item in words)
            {
                if (!str.ToLower().Contains(item.ToLower()))
                {
                    found = false;
                    break;
                }
                    
            }
            return found;
        }

    }
}
