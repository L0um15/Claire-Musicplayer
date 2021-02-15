using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Text;

namespace Claire_Musicplayer.Services.Audio
{
    public class AudioManager : IDisposable
    {

        private readonly WaveOutEvent _outputDevice;
        private ForgedFileReader _forgedFileReader;

        /// <summary>
        /// Track path location
        /// </summary>
        public string CurrentTrack { get; private set; }
        public int Volume
        {
            get => (int)MathF.Round(_outputDevice.Volume * 100);
            set { _outputDevice.Volume = (float)value / 100; }
        }

        public AudioManager()
        {
            _outputDevice = new WaveOutEvent();
        }

        public void Play(string input = null)
        {
            if (_outputDevice.PlaybackState == PlaybackState.Playing)
                Stop();
            if (_outputDevice.PlaybackState == PlaybackState.Paused)
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
}
