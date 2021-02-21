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
                string[] entries = Directory.GetFileSystemEntries(DirectoryHelper.CurrentDirectory, "*", SearchOption.TopDirectoryOnly)
                    .Where(e => !File.GetAttributes(e).HasFlag(FileAttributes.Hidden))
                    .OrderBy(e=> !File.GetAttributes(e).HasFlag(FileAttributes.Directory)).ToArray();

                Paginator<string> paginator = new Paginator<string>(entries, System.Console.BufferHeight - 4);

                if (!paginator.IsValidPage(value))
                {
                    Console.WriteLine("Page was out of range");
                    return;
                }

                var page = paginator.GetPage(value);

                for(int i = 0; i < page.Span.Length; i++)
                {
                    FileAttributes attr = File.GetAttributes(page.Span[i]);
                    if(attr == FileAttributes.Directory)
                    {
                        Console.WriteLine($"<DIR> {Path.GetFileName(page.Span[i])}");
                    }
                    else
                    {
                        Console.WriteLine($"      {Path.GetFileName(page.Span[i])}");
                    }
                }
                Console.WriteLine($"PAGE  {page.PageNumber} of {paginator.PageCount}");
            }
            else
            {
                Console.WriteLine("Accepts only integer!");
            }   
        }

        public string[] GetManual()
        {
            return new string[] {
                $"{GetName()} - {Help()}",
                "Needs: <integer>",
                $"Usage: {GetName()} 2",
                "Description: This command is using paginator.",
                "To move between pages you need to specify integer from 1 to (N)",
                "Lists every file in current directory"
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
