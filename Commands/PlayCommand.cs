using Claire_Musicplayer.Interfaces;
using Claire_Musicplayer.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Claire_Musicplayer.Commands
{
    public class PlayCommand : ICommander
    {
        private readonly AudioHandler _audioHandler;

        public PlayCommand(AudioHandler audioHandler)
        {
            _audioHandler = audioHandler;
        }
        public void Execute(string[] args)
        {
            _audioHandler.Play();
            MessageExtensions.WriteLine("Playing");
        }

        public string Help()
        {
            return "Play's track";
        }

        public string Invoke()
        {
            return "play";
        }
    }
}
