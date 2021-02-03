using System;
using System.Collections.Generic;
using System.Text;

namespace Claire_Musicplayer
{
    public class TrackInfo
    {
        public uint TrackNumber;
        public uint DiskNumber;
        public uint Year;
        public string Artist;
        public string Title;
        public string Lyrics;
        public string Genres;
        public string Album;
        public string Comments;
        public string Copyright;
    }

    public class MetaFactory : IDisposable
    {
        private readonly TagLib.File _file;
        public MetaFactory(string path)
        {
            _file = TagLib.File.Create(path);
        }

        public TrackInfo GetInfo()
        {
            return new TrackInfo()
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
        }

        public void Dispose()
        {
            _file.Dispose();
        }

    }
}
