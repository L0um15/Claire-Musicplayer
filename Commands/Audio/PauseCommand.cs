﻿using Claire_Musicplayer.Interfaces;
using Claire_Musicplayer.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Claire_Musicplayer.Commands.Audio
{
    public class PauseCommand : ICommander
    {

        private readonly AudioHandler _audioHandler;

        public PauseCommand(AudioHandler audioHandler)
        {
            _audioHandler = audioHandler;
        }

        public void Execute(ReadOnlySpan<string> args)
        {
            _audioHandler.Pause();
            MessageExtensions.WriteLine("Paused!");
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
