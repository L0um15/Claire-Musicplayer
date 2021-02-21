using Claire_Musicplayer.Interfaces;
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

            for (int j = 0; j < helpPage.Length; j++)
                Console.WriteLine("  " + helpPage[j]);
            
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
