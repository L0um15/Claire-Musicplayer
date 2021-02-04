using Claire_Musicplayer.Interfaces;
using Claire_Musicplayer.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Claire_Musicplayer.Commands.Movement
{
    public class ChangeDirCommand : ICommander
    {
        public void Execute(ReadOnlySpan<string> args)
        {
            if (args.Length == 0) return;

            string path = string.Join(' ', args.ToArray()); // :(

            DirectoryHelper.TrySetCurrentDirectory(path);
        }

        public string Help()
        {
            return "Changes directory";
        }

        public string GetName()
        {
            return "cd";
        }
    }
}
