using Claire_Musicplayer.Interfaces;
using Pastel;
using System;
using System.Collections.Generic;
using System.Text;

namespace Claire_Musicplayer.Commands.Terminal
{
    public class HelpCommand : ICommander
    {
        public void Execute(ReadOnlySpan<string> args)
        {

            string[] helpPage = new string[CommandHandler.commands.Count];
            
            int i = 0;
            
            foreach (KeyValuePair<string, ICommander> keyValuePair in CommandHandler.commands) 
            {
                helpPage[i] = $"{keyValuePair.Key} - {keyValuePair.Value.Help()}";
                i++;
            }
            Console.WriteLine("┌[".Pastel("#F568CE") + "HelpPage" + "]".Pastel("#F568CE"));
            for (int j = 0; j < helpPage.Length; j++)
                Console.WriteLine("│ ".Pastel("#F568CE") + helpPage[j]);
            Console.WriteLine("└[".Pastel("#F568CE") + "EndHelp" + "]".Pastel("#F568CE"));
            
        }

        public string[] GetManual()
        {
            return new string[] {
                $"{GetName()} - {Help()}",
                "Needs: Nothing",
                $"Usage: {GetName()}",
                "Description: Displays every loaded command."
            };
        }

        public string GetName()
        {
            return "help";
        }

        public string Help()
        {
            return "Displays help page";
        }
    }
}
