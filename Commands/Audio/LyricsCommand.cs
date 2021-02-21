using Claire_Musicplayer.Interfaces;
using Claire_Musicplayer.Services.Audio;
using System;
using System.Collections.Generic;
using System.Text;

namespace Claire_Musicplayer.Commands.Audio
{
    public class LyricsCommand : ICommander
    {
        private readonly AudioManager _audioManager;

        public LyricsCommand(AudioManager audioManager)
        {
            _audioManager = audioManager;
        }

        public void Execute(ReadOnlySpan<string> args)
        {

            if (_audioManager.CurrentTrack == null)
            {
                Console.WriteLine("Currently nothing is playing.");
                return;
            }

            string lyrics = Utilities.GetTrackInfo(_audioManager.CurrentTrack).Lyrics;

            if(lyrics == null)
            {
                Console.WriteLine("Sorry, this track does not contains any lyrics");
                return;
            }

            Console.WriteLine(lyrics);

        }

        public string[] GetManual()
        {
            return new string[] {
                $"{GetName()} - {Help()}",
                "Needs: Nothing",
                $"Usage: {GetName()}",
                "Description: Attempts to get encoded lyrics from track meta"
            };
        }

        public string GetName()
        {
            return "lyrics";
        }

        public string Help()
        {
            return "Extract's and displays lyrics from audiofile.";
        }
    }
}
