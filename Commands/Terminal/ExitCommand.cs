using Claire_Musicplayer.Interfaces;
using Claire_Musicplayer.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Claire_Musicplayer.Commands.Terminal
{
    public class ExitCommand : ICommander
    {

        private readonly AudioHandler _audioHandler;
        public ExitCommand(AudioHandler audioHandler)
        {
            _audioHandler = audioHandler;
        }

        public void Execute(ReadOnlySpan<string> args)
        {
            _audioHandler.Dispose();
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
