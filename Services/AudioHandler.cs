using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Text;

namespace Claire_Musicplayer.Services
{
    public class AudioHandler : IDisposable
    {

        private readonly WaveOutEvent _outputDevice;

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
            var audioFile = new AudioFileReader(input);
            CurrentTrack = input;
            _outputDevice.Init(new AutoDisposableFileReader(audioFile));
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

        public void Dispose()
        {
            _outputDevice.Dispose();
        }
    }

    /// <summary>
    /// Disposes files after playback has stopped or finished.
    /// </summary>
    public class AutoDisposableFileReader : ISampleProvider
    {
        private AudioFileReader _audioFileReader;
        private bool IsDisposed;

        public AutoDisposableFileReader(AudioFileReader audioFileReader)
        {
            _audioFileReader = audioFileReader;
            WaveFormat = audioFileReader.WaveFormat;
        }

        public int Read(float[] buffer, int offset, int count)
        {
            if (IsDisposed)
                return 0;

            int read = _audioFileReader.Read(buffer, offset, count);
            if(read == 0)
            {
                _audioFileReader.Dispose();
                IsDisposed = true;
            }
            return read;
        }
        public WaveFormat WaveFormat { get; private set; }
    }
}
