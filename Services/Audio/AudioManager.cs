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

        public PlaybackState CurrentState => _outputDevice.PlaybackState;
        public bool HasUserInterrupted { get; set; }
        public string CurrentTrack { get; private set; }
        public int Volume
        {
            get => (int)MathF.Round(_outputDevice.Volume * 100);
            set { _outputDevice.Volume = (float)value / 100; }
        }

        public AudioManager()
        {
            _outputDevice = new WaveOutEvent();
            _outputDevice.PlaybackStopped += _outputDevice_PlaybackStopped;
        }

        private void _outputDevice_PlaybackStopped(object sender, StoppedEventArgs e)
        {
            if (!HasUserInterrupted)
            {
                List<string> tracks = DirectoryHelper.Tracklist;
                int index = tracks.IndexOf(CurrentTrack);
                index++;
                if (index < tracks.Count)
                {
                    CurrentTrack = tracks[index];
                    Load(CurrentTrack);
                    Play();
                }
            }
        }

        public void Load(string input)
        {
            if (CurrentState != PlaybackState.Stopped)
            {
                HasUserInterrupted = true; // Obviously user interrupted during playback
                if (!_forgedFileReader.IsDisposed)
                    _forgedFileReader.Dispose();
                _outputDevice.Stop();
            }
            _forgedFileReader = new ForgedFileReader(input);
            _outputDevice.Init(_forgedFileReader);
            CurrentTrack = input;
        }

        public void Play()
        {
            if (CurrentState != PlaybackState.Playing)
            {
                if (_forgedFileReader != null)
                {
                    if (!_forgedFileReader.IsDisposed)
                    {
                        HasUserInterrupted = false;
                        _outputDevice.Play();
                    }
                }
            }
                
        }

        public void Pause()
        {
            if(CurrentState == PlaybackState.Playing)
                _outputDevice.Pause();
        }

        public void Stop()
        {
            if(CurrentState != PlaybackState.Stopped)
            {
                HasUserInterrupted = true;
                _outputDevice.Stop();
                if (!_forgedFileReader.IsDisposed)
                    _forgedFileReader.Dispose();
                CurrentTrack = null;
            }
        }

        public TimeSpan GetPositionAsTimeSpan()
        {
            return _forgedFileReader.Position;
        }

        public TimeSpan GetDurationAsTimeSpan()
        {
            return _forgedFileReader.Duration;
        }

        public void Dispose()
        {
            _outputDevice.Dispose();
        }
    }
}
