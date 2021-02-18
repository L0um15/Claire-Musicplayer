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
            int pageNum = 1;
            if (args.Length > 0)
            {
                if (int.TryParse(args[0], out int value))
                {
                    pageNum = value;
                }
                else
                {
                    MessageExtensions.WriteLine("Help page requires an integer as argument");
                    return;
                }
            }

            string[] helpPage = new string[CommandHandler.commands.Count];
            
            int i = 0;
            
            foreach (KeyValuePair<string, ICommander> keyValuePair in CommandHandler.commands) 
            {
                helpPage[i] = $"{keyValuePair.Key} - {keyValuePair.Value.Help()}";
                i++;
            }

            Paginator<string> paginator = new Paginator<string>(helpPage, Console.BufferHeight - 4);

            if (!paginator.IsValidPage(pageNum))
            {
                MessageExtensions.WriteLine("Page was out of range");
                return;
            }

            var page = paginator.GetPage(pageNum);

            for (int j = 0; j < page.Span.Length; j++)
            {
                MessageExtensions.WriteLine(helpPage[j]);
            }
            MessageExtensions.WriteLine($"PAGE  {page.PageNumber} of {paginator.PageCount}");


        }

        public string[] GetManual()
        {
            return new string[] {
                $"{GetName()} - {Help()}",
                "Needs: <integer>",
                $"Usage: {GetName()} 2",
                "Description: This command is using paginator.",
                "To move between pages you need to specify integer from 1 to (N)",
                "Displays every loaded command."
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
