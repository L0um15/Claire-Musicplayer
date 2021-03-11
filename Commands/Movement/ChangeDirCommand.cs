using Claire.Interfaces;
using Claire.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Claire.Commands.Movement
{
    public class ChangeDirCommand : ICommander
    {
        public void Execute(ReadOnlySpan<string> args)
        {
            if (args.Length == 0) return;

            string path = string.Join(' ', args.ToArray()); // :(

            if (DirectoryHelper.TrySetCurrentDirectory(path))
                DirectoryHelper.RefreshTracklist();
        }

        public string[] GetManual()
        {
            return new string[] {
                $"{GetName()} - {Help()}",
                "Needs: <string>",
                $"Usage: {GetName()} C:\\Users\\default user\\music",
                "Description: Changes current directory",
                "Path can be absolute: C:\\Users\\defaultuser\\music",
                "Or relative: ../../../music/",
                "Whitespaces are allowed: C:\\Users\\default user\\music"
            };
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
