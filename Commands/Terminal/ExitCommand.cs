using Claire_Musicplayer.Interfaces;
using Claire_Musicplayer.Services.Audio;
using System;
using System.Collections.Generic;
using System.Text;

namespace Claire_Musicplayer.Commands.Terminal
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
