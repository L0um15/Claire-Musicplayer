using Claire_Musicplayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Claire_Musicplayer.Commands.Terminal
{
    public class ManCommand : ICommander
    {
        public void Execute(ReadOnlySpan<string> args)
        {
            throw new NotImplementedException();
        }

        public string[] GetMan()
        {
            return new string[] {
                $"{GetName()} - {Help()}",
                $"Usage: {GetName()} <command>",
                "Returns: Detailed information about passed argument"
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
