using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Text;

namespace Claire_Musicplayer.Services.Audio
{
    /// <summary>
    /// Automatic AudioFileReader Disposal.
    /// </summary>
    public class ForgedFileReader : ISampleProvider, IDisposable
    {
        private readonly AudioFileReader _source;
        public bool IsDisposed { get; private set; }

        public ForgedFileReader(string input)
        {
            _source = new AudioFileReader(input);
        }

        public TimeSpan Position
            => _source.CurrentTime;

        public TimeSpan Duration
            => _source.TotalTime;

        public WaveFormat WaveFormat => _source.WaveFormat;

        public void Dispose()
        {
            if (_source != null)
            {
                IsDisposed = true;
                _source.Dispose();
            }
        }

        public int Read(float[] buffer, int offset, int count)
        {
            if (IsDisposed)
                return 0;

            int read = _source.Read(buffer, offset, count);
            if (read == 0)
            {
                Dispose();
            }
            return read;
        }
    }
}
