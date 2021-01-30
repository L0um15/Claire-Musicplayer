using Claire_Musicplayer.Interfaces;
using Claire_Musicplayer.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Claire_Musicplayer.Commands.Audio
{
    public class StopCommand : ICommander
    {

        private readonly AudioHandler _audioHandler;

        public StopCommand(AudioHandler audioHandler)
        {
            _audioHandler = audioHandler;
        }

        public void Execute(string[] args)
        {
            _audioHandler.Stop();
            MessageExtensions.WriteLine("Stopped!");
        }

        public string Help()
        {
            return "Stops playback";
        }

        public string Invoke()
        {
            return "stop";
        }
    }
}
