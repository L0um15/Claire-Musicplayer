using Claire_Musicplayer.Interfaces;
using Claire_Musicplayer.Models;
using Claire_Musicplayer.Services.Audio;
using System;
using System.Collections.Generic;
using System.Text;

namespace Claire_Musicplayer.Commands.Audio
{
    public class InfoCommand : ICommander
    {

        private readonly AudioManager _audioManager;

        public InfoCommand(AudioManager audioManager)
        {
            _audioManager = audioManager;
        }

        public void Execute(ReadOnlySpan<string> args)
        {
            if(_audioManager.CurrentTrack == null)
            {
                MessageExtensions.WriteLine("Currently nothing is playing.");
                return;
            }

            TrackInfo trackInfo = GetInfo(_audioManager.CurrentTrack);

            MessageExtensions.Clear();
            MessageExtensions.WriteLine($"Track: {trackInfo.Artist} - {trackInfo.Title}");
            MessageExtensions.WriteLine($"Album: {trackInfo.Album}");
            MessageExtensions.WriteLine($"Genres: {trackInfo.Genres}");
            MessageExtensions.WriteLine($"Copyright: {trackInfo.Copyright}");
            MessageExtensions.WriteLine($"Year: {trackInfo.Year}");
            MessageExtensions.WriteLine($"Track Number: {trackInfo.TrackNumber}");
            MessageExtensions.WriteLine($"Disc Number: {trackInfo.DiskNumber}");
            MessageExtensions.WriteLine($"Pos/Dur: {_audioManager.GetPosition()} / {_audioManager.GetDuration()}");
        }

        public static TrackInfo GetInfo(string path)
        {
            var _file = TagLib.File.Create(path);
            TrackInfo trackInfo = new TrackInfo()
            {
                Artist = _file.Tag.Artists.Length > 0 ? _file.Tag.Artists[0] : null,
                Title = _file.Tag.Title,
                Album = _file.Tag.Album,
                Genres = _file.Tag.Genres.Length > 0 ? _file.Tag.Genres[0] : null,
                Lyrics = _file.Tag.Lyrics,
                Comments = _file.Tag.Comment,
                Copyright = _file.Tag.Copyright,
                TrackNumber = _file.Tag.Track,
                DiskNumber = _file.Tag.Disc,
                Year = _file.Tag.Year
            };
            _file.Dispose();
            return trackInfo;
        }
        public string Help()
        {
            return "Displays Track information";
        }

        public string GetName()
        {
            return "info";
        }
    }
}
