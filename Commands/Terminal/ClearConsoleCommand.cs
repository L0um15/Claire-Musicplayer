using Claire_Musicplayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Claire_Musicplayer.Commands.Terminal
{
    public class ClearConsoleCommand : ICommander
    {
        public void Execute(string[] args)
        {
            MessageExtensions.Clear();
        }

        public string Help()
        {
            return "Clears screen";
        }

        public string Invoke()
        {
            return "clear";
        }
    }
}
