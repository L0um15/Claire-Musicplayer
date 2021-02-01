using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Text;

namespace Claire_Musicplayer.Services
{
    public class AudioHandler : IDisposable
    {

        private readonly WaveOutEvent _outputDevice;
        private ForgedFileReader _forgedFileReader;

        /// <summary>
        /// Track path location
        /// </summary>
        public string CurrentTrack { get; private set; }
        public int Volume 
        {
            get => (int)Math.Round((decimal)_outputDevice.Volume * 100);
            set { _outputDevice.Volume = (float)value / 100; }
        }

        public AudioHandler()
        {
            _outputDevice = new WaveOutEvent();
        }

        public void Play(string input = null)
        {
            if(_outputDevice.PlaybackState == PlaybackState.Playing)
                Stop();
            if(_outputDevice.PlaybackState == PlaybackState.Paused)
            {
                _outputDevice.Play();
                return;
            }
            if (input == null) return;
            _forgedFileReader = new ForgedFileReader(input);
            CurrentTrack = input;
            _outputDevice.Init(_forgedFileReader);
            _outputDevice.Play();
        }

        public void Pause()
        {
            _outputDevice.Pause();
        }

        public void Stop()
        {
            _outputDevice.Stop();
            CurrentTrack = null;
        }

        public string GetPosition()
        {
            return _forgedFileReader.Position.ToString("mm\\:ss");
        }

        public string GetDuration()
        {
            return _forgedFileReader.Duration.ToString("mm\\:ss");
        }

        public void Dispose()
        {
            _outputDevice.Dispose();
        }
    }

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
