using Claire.Interfaces;
using Claire.Services;
using Claire.Services.Audio;
using ManagedBass;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Claire.Commands.Audio
{
    public class PlayCommand : ICommander
    {
        private readonly AudioManager _audioManager;

        public PlayCommand(AudioManager audioManager)
        {
            _audioManager = audioManager;
        }
        public void Execute(ReadOnlySpan<string> args)
        {
            if (args.Length == 0)
            {
                _audioManager.Play();
                return;
            }

            if (args[0] == string.Empty)
                return;

            List<string> tracks = DirectoryHelper.Tracklist;

            bool matchFound = false;
            foreach(string item in tracks)
            {
                if (item.ContainsAll(args))
                {
                    matchFound = true;
                    _audioManager.Load(item);
                    _audioManager.Play();
                    break;
                }
            }

            if(_audioManager.CurrentState == PlaybackState.Playing && matchFound)
                Console.WriteLine("Playing. Type \"info\" for details.");
        }

        public string[] GetManual()
        {
            return new string[] {
                $"{GetName()} - {Help()}",
                "Needs: <strings>",
                $"Usage: {GetName()} bury the light",
                "Description: Initializes playback for given input",
                "Move to track dir by using 'cd' command",
                "Partial matching is supported: 'b he lig'",
                "However typos are not."
            };
        }

        public string Help()
        {
            return "Starts Playback";
        }

        public string GetName()
        {
            return "play";
        }
    }
}
