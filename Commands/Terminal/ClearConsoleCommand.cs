using Claire.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Claire.Commands.Terminal
{
    public class ClearConsoleCommand : ICommander
    {
        public void Execute(ReadOnlySpan<string> args)
        {
            Console.Clear();
        }

        public string[] GetManual()
        {
            return new string[] {
                $"{GetName()} - {Help()}",
                "Needs: Nothing",
                $"Usage: {GetName()}",
                "Description: Clears console output without wiping history."
            };
        }

        public string Help()
        {
            return "Clear's Screen";
        }

        public string GetName()
        {
            return "clear";
        }
    }
}
