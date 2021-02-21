using Claire_Musicplayer.Interfaces;
using Pastel;
using System;
using System.Collections.Generic;
using System.Text;

namespace Claire_Musicplayer.Commands.Terminal
{
    public class ManCommand : ICommander
    {
        public void Execute(ReadOnlySpan<string> args)
        {
            if(args.Length == 0)
                return;

            if (CommandHandler.commands.TryGetValue(args[0], out ICommander command))
            {
                string[] man = command.GetManual();
                Console.WriteLine("┌[".Pastel("#F568CE") +"ManPage"+"]".Pastel("#F568CE"));
                for(int i = 0; i < man.Length; i++)
                    Console.WriteLine("│ ".Pastel("#F568CE") + man[i]);
                Console.WriteLine("└[".Pastel("#F568CE") +"EndPage"+"]".Pastel("#F568CE"));
            }
            else
            {
                Console.WriteLine("Manual for this command does not exist.");
            }

        }

        public string[] GetManual()
        {
            return new string[] {
                $"{GetName()} - {Help()}",
                "Needs: <command>",
                $"Usage: {GetName()} ls",
                "Description: Gives detailed information about passed command"
            };
        }

        public string GetName()
        {
            return "man";
        }

        public string Help()
        {
            return "Commands manual";
        }
    }
}
