using Claire_Musicplayer.Interfaces;
using Claire_Musicplayer.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Claire_Musicplayer.Commands.Movement
{
    public class ListCommand : ICommander
    {
        public void Execute(ReadOnlySpan<string> args)
        {

            string[] entries = Directory.GetFileSystemEntries(DirectoryHelper.CurrentDirectory, "*", SearchOption.TopDirectoryOnly)
                    .Where(e => !File.GetAttributes(e).HasFlag(FileAttributes.Hidden))
                    .OrderBy(e => !File.GetAttributes(e).HasFlag(FileAttributes.Directory)).ToArray();

            for (int i = 0; i < entries.Length; i++)
            {
                FileAttributes attr = File.GetAttributes(entries[i]);
                if (attr == FileAttributes.Directory)
                {
                    Console.WriteLine($"<DIR> {Path.GetFileName(entries[i])}");
                }
                else
                {
                    Console.WriteLine($"      {Path.GetFileName(entries[i])}");
                }
            }

        }

        public string[] GetManual()
        {
            return new string[] {
                $"{GetName()} - {Help()}",
                "Needs: Nothing",
                $"Usage: {GetName()}",
                "Description: Lists every file in current directory"
            };
        }

        public string Help()
        {
            return "Shows Directory structure";
        }

        public string GetName()
        {
            return "ls";
        }
    }
}
