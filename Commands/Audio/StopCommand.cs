using Claire_Musicplayer.Interfaces;
using Claire_Musicplayer.Services.Audio;
using System;
using System.Collections.Generic;
using System.Text;

namespace Claire_Musicplayer.Commands.Audio
{
    public class StopCommand : ICommander
    {

        private readonly AudioManager _audioManager;

        public StopCommand(AudioManager audioManager)
        {
            _audioManager = audioManager;
        }

        public void Execute(ReadOnlySpan<string> args)
        {
            _audioManager.Stop();
            MessageExtensions.WriteLine("Stopped!");
        }

        public string[] GetManual()
        {
            return new string[] {
                $"{GetName()} - {Help()}",
                "Needs: Nothing",
                $"Usage: {GetName()}",
                "Description: Stops current playback",
                "Disables continous playlist playback"
            };
        }

        public string Help()
        {
            return "Stops Playback";
        }

        public string GetName()
        {
            return "stop";
        }
    }
}
