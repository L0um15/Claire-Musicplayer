﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Claire.Services
{
    public static class DirectoryHelper
    {
        public static string CurrentDirectory { get; private set; } = Directory.GetCurrentDirectory();
        public static string MusicDirectory { get => Environment.GetFolderPath(Environment.SpecialFolder.MyMusic); }
        public static string UserProfile { get => Environment.GetFolderPath(Environment.SpecialFolder.UserProfile); }

        public readonly static List<string> Tracklist = new List<string>();

        /// <summary>
        /// Will attempt to change directory and handle DirectoryNotFoundException
        /// </summary>
        /// <param name="path">Can be absolute or relative path</param>
        public static bool TrySetCurrentDirectory(string path)
        {
            try
            {
                if (path.Contains('~'))
                    path = path.Replace("~", UserProfile);

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
                Console.WriteLine($"Directory: \"{path}\" not found");
            }
            return false;
        }

        /// <summary>
        /// Clears and updates Tracklist
        /// </summary>
        public static void RefreshTracklist()
        {
            Tracklist.Clear();
            string[] allFiles = Directory.GetFiles(CurrentDirectory, "*", SearchOption.TopDirectoryOnly);
            var span = Utilities.AllowedMediaExtensions.AsSpan();
            for (int i = 0; i < allFiles.Length; i++)
            {
                if (Utilities.Contains(span,Path.GetExtension(allFiles[i]), StringComparison.InvariantCultureIgnoreCase))
                {
                    Tracklist.Add(allFiles[i]);
                }
            }
        }
    }
}
