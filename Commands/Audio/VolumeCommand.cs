using Claire_Musicplayer.Interfaces;
using Claire_Musicplayer.Services.Audio;
using System;
using System.Collections.Generic;
using System.Text;

namespace Claire_Musicplayer.Commands.Audio
{
    public class VolumeCommand : ICommander
    {

        private readonly AudioManager _audioManager;

        public VolumeCommand(AudioManager _audioManager)
        {
            this._audioManager = _audioManager;
        }

        public void Execute(ReadOnlySpan<string> args)
        {
            
            if(args.Length == 0)
            {
                MessageExtensions.WriteLine($"Current Volume: {_audioManager.Volume}%");
                return;
            }

            if(args.Length > 0)
            {
                if (int.TryParse(args[0], out int value))
                {
                    if (value >= 0 && value <= 100)
                    {
                        _audioManager.Volume = value;
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
            return "Display's / Changes Volume level";
        }

        public string GetName()
        {
            return "vol";
        }
    }
}
