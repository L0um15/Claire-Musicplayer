using Claire_Musicplayer.Interfaces;
using Claire_Musicplayer.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Claire_Musicplayer.Commands.Audio
{
    public class VolumeCommand : ICommander
    {

        private readonly AudioHandler _audioHandler;

        public VolumeCommand(AudioHandler audioHandler)
        {
            _audioHandler = audioHandler;
        }

        public void Execute(ReadOnlySpan<string> args)
        {
            
            if(args.Length == 0)
            {
                MessageExtensions.WriteLine($"Current Volume: {_audioHandler.Volume}%");
                return;
            }

            if(args.Length > 0)
            {
                if (int.TryParse(args[0], out int value))
                {
                    if (value >= 0 && value <= 100)
                    {
                        _audioHandler.Volume = value;
                        MessageExtensions.WriteLine($"Volume changed to: {value}%");
                    }
                    else
                        MessageExtensions.WriteLine("Allowed range: 0-100%");
                    
                }
                else
                    MessageExtensions.WriteLine("Volume must be an integer");
            }

        }

        public string Help()
        {
            return "Display / Change Volume";
        }

        public string GetName()
        {
            return "vol";
        }
    }
}
