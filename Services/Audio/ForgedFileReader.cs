using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Text;

namespace Claire_Musicplayer.Services.Audio
{
    /// <summary>
    /// Automatic AudioFileReader Disposal.
    /// </summary>
    public class ForgedFileReader : ISampleProvider
    {
        private readonly AudioFileReader _source;
        private bool IsDisposed;

        public ForgedFileReader(string input)
        {
            _source = new AudioFileReader(input);
        }

        public TimeSpan Position
            => _source.CurrentTime;

        public TimeSpan Duration
            => _source.TotalTime;

        public WaveFormat WaveFormat => _source.WaveFormat;

        public int Read(float[] buffer, int offset, int count)
        {
            if (IsDisposed)
                return 0;

            int read = _source.Read(buffer, offset, count);
            if (read == 0)
            {
                _source.Dispose();
                IsDisposed = true;
            }
            return read;
        }
    }
}
