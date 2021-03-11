using Claire.Interfaces;
using Claire.Services.Audio;
using System;
using System.Collections.Generic;
using System.Text;

namespace Claire.Commands.Terminal
{
    public class ExitCommand : ICommander
    {

        private readonly AudioManager _audioManager;
        public ExitCommand(AudioManager audioManager)
        {
            _audioManager = audioManager;
        }

        public void Execute(ReadOnlySpan<string> args)
        {
            _audioManager.Dispose();
            Environment.Exit(0);
        }

        public string[] GetManual()
        {
            return new string[] {
                $"{GetName()} - {Help()}",
                "Needs: Nothing",
                $"Usage: {GetName()}",
                "Description: Disposes everything that is running then closes program."
            };
        }

        public string GetName()
        {
            return "exit";
        }

        public string Help()
        {
            return "Safely closes program";
        }
    }
}
