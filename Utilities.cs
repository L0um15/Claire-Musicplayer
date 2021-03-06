﻿using Claire.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;

namespace Claire
{
    public static class Utilities
    {

        public readonly static string[] AllowedMediaExtensions = {
            ".mp3", ".m4a", ".wav",
            ".aiff", ".ogg"
        };

        public static bool IsLinux => RuntimeInformation.IsOSPlatform(OSPlatform.Linux);

        public static string GetLatestVersion()
        {
            try
            {
                string result = new WebClient().DownloadString("https://raw.githubusercontent.com/L0um15/Claire-Musicplayer/main/version.txt");
                if (result != GetVersion())
                {
                    return result;
                }
            }
            catch (WebException) { }
            return null;
        }

        public static string GetVersion()
            => Assembly.GetExecutingAssembly().GetName().Version.ToString();


        public static TrackInfo GetTrackInfo(string path)
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

        public static bool Contains(ReadOnlySpan<string> span, string str, StringComparison comparison)
        {
            foreach (var s in span)
            {
                if(s.Equals(str, comparison))
                {
                    return true;
                }
            }
            return false;
        }

        public static bool ContainsAll(this string str, ReadOnlySpan<string> words)
        {
            bool found = true;
            foreach (string item in words)
            {
                if (!str.Contains(item, StringComparison.InvariantCultureIgnoreCase))
                {
                    found = false;
                    break;
                }
            }
            return found;
        }
    }
}
