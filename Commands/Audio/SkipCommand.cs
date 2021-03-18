using Claire.Interfaces;
using Claire.Services;
using Claire.Services.Audio;
using System;
using System.Collections.Generic;
using System.Text;

namespace Claire.Commands.Audio
{
    public class SkipCommand : ICommander
    {

        private readonly AudioManager _audioManager;

        public SkipCommand(AudioManager audioManager)
        {
            _audioManager = audioManager;
        }

        public void Execute(ReadOnlySpan<string> args)
        {
            List<string> tracks = DirectoryHelper.Tracklist;

            int index = tracks.IndexOf(_audioManager.CurrentTrack);

            if(index < tracks.Count)
            {
                _audioManager.Load(tracks[index + 1]);
                _audioManager.Play();
            }
        }

        public string[] GetManual()
        {
            return new string[] {
                $"{GetName()} - {Help()}",
                "Needs: Nothing",
                $"Usage: {GetName()}",
                "Description: Plays next track if available"
            };
        }

        public string GetName()
        {
            return "skip";
        }

        public string Help()
        {
            return "Skip track";
        }
    }
}
