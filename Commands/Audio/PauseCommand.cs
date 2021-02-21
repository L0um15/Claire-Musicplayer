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
            Console.WriteLine("Paused!");
        }

        public string[] GetManual()
        {
            return new string[] {
                $"{GetName()} - {Help()}",
                "Needs: Nothing",
                $"Usage: {GetName()}",
                "Description: Pauses playback"
            };
        }

        public string Help()
        {
            return "Pauses playback";
        }

        public string GetName()
        {
            return "pause";
        }
    }
}
