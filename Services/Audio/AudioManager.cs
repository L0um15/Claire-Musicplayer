using ManagedBass;
using ManagedBass.Wasapi;
using System;
using System.Collections.Generic;
using System.Text;

namespace Claire.Services.Audio
{
    public class AudioManager : IDisposable
    {

        private int _stream;
        private bool WasapiInitialized;
        public PlaybackState CurrentState => Bass.ChannelIsActive(_stream);
        public string CurrentTrack { get; private set; } // Always absolute path to file
        public int Volume
        {
            get
            {
                if (WasapiInitialized)
                    return (int)MathF.Round(BassWasapi.GetVolume(WasapiVolumeTypes.Session) * 100);
                else
                    return (int)Math.Round(Bass.Volume * 100);
            }
            set
            {
                if (WasapiInitialized)
                    BassWasapi.SetVolume(WasapiVolumeTypes.Session, (float) value / 100);
                else
                    Bass.Volume = (double) value / 100;
            }
        }

        public AudioManager()
        {
            if (!Bass.Init(Bass.DefaultDevice))
                throw new BassException();

            if (!Utilities.IsLinux)
                if (BassWasapi.Init(BassWasapi.DefaultDevice))
                    WasapiInitialized = true;
        }

        public void Load(string input)
        {
            if(CurrentState != PlaybackState.Stopped)
                Stop();
            _stream = Bass.CreateStream(input);
            Bass.ChannelSetSync(_stream, SyncFlags.End, 0, new SyncProcedure(PlaybackEnded));
            CurrentTrack = input;
        }

        private void PlaybackEnded(int Handle, int Channel, int Data, IntPtr User)
        {
            List<string> tracks = DirectoryHelper.Tracklist;
            int index = tracks.IndexOf(CurrentTrack);
            index++;
            if(index < tracks.Count)
            {
                CurrentTrack = tracks[index];
                Load(tracks[index]);
                Play();
            }
        }

        public void Play()
        {
            if(CurrentState != PlaybackState.Playing)
                Bass.ChannelPlay(_stream); // Start playback
        }
        
        public void Stop()
        {
            if(CurrentState != PlaybackState.Stopped)
            {
                Bass.ChannelStop(_stream); // Results in channel being freed and automatically removes SyncProcedure (PlaybackEnded)
                Bass.StreamFree(_stream);
                CurrentTrack = null;
            }
        }

        public void Pause()
        {
            if(CurrentState == PlaybackState.Playing)
                Bass.ChannelPause(_stream);
        }

        public TimeSpan GetPositionAsTimeSpan()
        {
            return TimeSpan.FromSeconds(Bass.ChannelBytes2Seconds(_stream, Bass.ChannelGetPosition(_stream)));
        }

        public TimeSpan GetDurationAsTimeSpan()
        {
            return TimeSpan.FromSeconds(Bass.ChannelBytes2Seconds(_stream, Bass.ChannelGetLength(_stream)));
        }



        public void Dispose()
        {
            Bass.Free();
            if(WasapiInitialized)
                BassWasapi.Free();
        }
    }
}
