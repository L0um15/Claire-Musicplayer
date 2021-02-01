using Claire_Musicplayer.Interfaces;
using Claire_Musicplayer.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Claire_Musicplayer.Commands.Audio
{
    public class InfoCommand : ICommander
    {

        private readonly AudioHandler _audioHandler;

        public InfoCommand(AudioHandler audioHandler)
        {
            _audioHandler = audioHandler;
        }

        public void Execute(string[] args)
        {
            if(_audioHandler.CurrentTrack == null)
            {
                MessageExtensions.WriteLine("Currently nothing is playing.");
                return;
            }

            using (MetaFactory metaFactory = new MetaFactory(_audioHandler.CurrentTrack))
            {
                TrackInfo trackInfo = metaFactory.GetInfo();
                MessageExtensions.Clear();
                MessageExtensions.WriteLine($"Track: {trackInfo.Artist} - {trackInfo.Title}");
                MessageExtensions.WriteLine($"Album: {trackInfo.Album}");
                MessageExtensions.WriteLine($"Genres: {trackInfo.Genres}");
                MessageExtensions.WriteLine($"Copyright: {trackInfo.Copyright}");
                MessageExtensions.WriteLine($"Year: {trackInfo.Year}");
                MessageExtensions.WriteLine($"Track Number: {trackInfo.TrackNumber}");
                MessageExtensions.WriteLine($"Disc Number: {trackInfo.DiskNumber}");
                MessageExtensions.WriteLine($"Pos/Dur: {_audioHandler.GetPosition()} / {_audioHandler.GetDuration()}");
            }
        }

        public string Help()
        {
            return "Display info about current track";
        }

        public string Invoke()
        {
            return "info";
        }
    }
}
