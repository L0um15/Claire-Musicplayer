using Claire_Musicplayer.Interfaces;
using Claire_Musicplayer.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Claire_Musicplayer.Commands.Movement
{
    public class ChangeDirCommand : ICommander
    {
        public void Execute(ReadOnlySpan<string> args)
        {
            if (args.Length == 0) return;

            string path = string.Join(' ', args.ToArray()); // :(

            if (DirectoryHelper.TrySetCurrentDirectory(path))
            {
                DirectoryHelper.Tracklist.Clear();
                string[] allFiles = Directory.GetFiles(path, "*", SearchOption.TopDirectoryOnly);
                for(int i = 0; i < allFiles.Length; i++)
                {
                    if (Utilities.AllowedMediaExtensions.Contains(Path.GetExtension(allFiles[i]).ToLower()))
                    {
                        DirectoryHelper.Tracklist.Add(allFiles[i]);
                    }
                }

            }
        }

        public string Help()
        {
            return "Changes Directory";
        }

        public string GetName()
        {
            return "cd";
        }
    }
}
