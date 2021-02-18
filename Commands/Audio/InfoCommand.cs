using Claire_Musicplayer.Interfaces;
using Claire_Musicplayer.Models;
using Claire_Musicplayer.Services.Audio;
using System;
using System.Collections.Generic;
using System.IO;
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

            TrackInfo trackInfo = Utilities.GetTrackInfo(_audioManager.CurrentTrack);

            TimeSpan currentTimeSpan = _audioManager.GetPositionAsTimeSpan();
            TimeSpan totalTimeSpan = _audioManager.GetDurationAsTimeSpan();

            string currentTimeFormat = string.Format(
                    "{0:D2}:{1:D2}:{2:D2}.{3:D2}ms",
                    currentTimeSpan.Hours,
                    currentTimeSpan.Minutes,
                    currentTimeSpan.Seconds,
                    currentTimeSpan.Milliseconds
                );
            string totalTimeFormat = string.Format(
                    "{0:D2}:{1:D2}:{2:D2}.{3:D2}ms",
                    totalTimeSpan.Hours,
                    totalTimeSpan.Minutes,
                    totalTimeSpan.Seconds,
                    totalTimeSpan.Milliseconds
                );

            MessageExtensions.Clear();

            string fullname;
            if (trackInfo.Artist == null || trackInfo.Title == null)
                fullname = Path.GetFileNameWithoutExtension(_audioManager.CurrentTrack);
            else
                fullname = $"{trackInfo.Artist} - {trackInfo.Title}";

            MessageExtensions.WriteLine($"Track: {fullname}");
            MessageExtensions.WriteLine($"Album: {trackInfo.Album}");
            MessageExtensions.WriteLine($"Genres: {trackInfo.Genres}");
            MessageExtensions.WriteLine($"Copyright: {trackInfo.Copyright}");
            MessageExtensions.WriteLine($"Year: {trackInfo.Year}");
            MessageExtensions.WriteLine($"Track Number: {trackInfo.TrackNumber}");
            MessageExtensions.WriteLine($"Disc Number: {trackInfo.DiskNumber}");
            MessageExtensions.WriteLine($"Pos/Dur: {currentTimeFormat} / {totalTimeFormat}");
        }

        public string[] GetManual()
        {
            return new string[] {
                $"{GetName()} - {Help()}",
                "Needs: Nothing",
                $"Usage: {GetName()}",
                "Description: Displays full detailed information about track artist, album, genres etc."
            };
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
