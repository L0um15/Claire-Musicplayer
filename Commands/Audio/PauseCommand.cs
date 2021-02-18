using Claire_Musicplayer.Interfaces;
using Claire_Musicplayer.Services.Audio;
using System;
using System.Collections.Generic;
using System.Text;

namespace Claire_Musicplayer.Commands.Audio
{
    public class PauseCommand : ICommander
    {

        private readonly AudioManager _audioManager;

        public PauseCommand(AudioManager audioManager)
        {
            _audioManager = audioManager;
        }

        public void Execute(ReadOnlySpan<string> args)
        {
            _audioManager.Pause();
            MessageExtensions.WriteLine("Paused!");
        }

        public string[] GetMan()
        {
            throw new NotImplementedException();
        }

        public string Help()
        {
            return "Pauses Playback";
        }

        public string GetName()
        {
            return "pause";
        }
    }
}
