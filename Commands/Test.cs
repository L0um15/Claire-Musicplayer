using Claire_Musicplayer.Interfaces;
using Claire_Musicplayer.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Claire_Musicplayer.Commands
{
    public class Test : ICommander
    {
        private readonly AudioHandler _audioHandler;

        public Test(AudioHandler audioHandler)
        {
            _audioHandler = audioHandler;
        }
        public void Execute(string[] args)
        {
            _audioHandler.Play();
        }

        public string Help()
        {
            return "simple test";
        }

        public string Invoke()
        {
            return "test";
        }
    }
}
