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

        public static bool ContainsAll(this string str, ReadOnlySpan<string> words)
        {
            bool found = true;
            foreach(string item in words)
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
