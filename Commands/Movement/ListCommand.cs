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
        public void Execute(string[] args)
        {

            string arg = args.Length > 0 ? args[0] : "0";

            if(int.TryParse(arg, out int value))
            {
                IList<string> entries = Directory.GetFileSystemEntries(DirectoryHelper.CurrentDirectory, "*", SearchOption.TopDirectoryOnly)
                    .Where(e => !File.GetAttributes(e).HasFlag(FileAttributes.Hidden))
                    .OrderBy(e=> !File.GetAttributes(e).HasFlag(FileAttributes.Directory))
                    .ToList();

                IList<string> page = entries.Paginate(value, Console.BufferHeight - 3);

                for(int i = 0; i < page.Count; i++)
                {
                    FileAttributes attr = File.GetAttributes(page[i]);
                    if(attr == FileAttributes.Directory)
                    {
                        MessageExtensions.WriteLine($"<DIR> {Path.GetFileName(page[i])}");
                    }
                    else
                    {
                        MessageExtensions.WriteLine($"      {Path.GetFileName(page[i])}");
                    }
                    
                }
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

        public string Invoke()
        {
            return "ls";
        }
    }
}
