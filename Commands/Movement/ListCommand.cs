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

            string arg = args.Length > 0 ? args[0] : "1";

            if(int.TryParse(arg, out int value))
            {
                List<string> entries = Directory.GetFileSystemEntries(DirectoryHelper.CurrentDirectory, "*", SearchOption.TopDirectoryOnly)
                    .Where(e => !File.GetAttributes(e).HasFlag(FileAttributes.Hidden))
                    .OrderBy(e=> !File.GetAttributes(e).HasFlag(FileAttributes.Directory))
                    .ToList();

                Paginator<string> paginator = new Paginator<string>(entries, Console.BufferHeight - 4);

                List<string> pageEntries = paginator.GetPage(value);

                for(int i = 0; i < pageEntries.Count; i++)
                {
                    FileAttributes attr = File.GetAttributes(pageEntries[i]);
                    if(attr == FileAttributes.Directory)
                    {
                        MessageExtensions.WriteLine($"<DIR> {Path.GetFileName(pageEntries[i])}");
                    }
                    else
                    {
                        MessageExtensions.WriteLine($"      {Path.GetFileName(pageEntries[i])}");
                    }
                }
                MessageExtensions.WriteLine($"PAGE  {paginator.PageNumber} of {paginator.PageCount}");
            }
            else
            {
                MessageExtensions.WriteLine("Accepts only integer!");
            }   
        }

        public string Help()
        {
            return "Shows directory structure";
        }

        public string GetName()
        {
            return "ls";
        }
    }
}
