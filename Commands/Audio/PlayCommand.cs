using Claire_Musicplayer.Interfaces;
using Claire_Musicplayer.Services;
using Claire_Musicplayer.Services.Audio;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Claire_Musicplayer.Commands.Audio
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

            List<string> tracks = Directory.GetFiles(DirectoryHelper.CurrentDirectory, "*", SearchOption.TopDirectoryOnly).ToList();

            foreach(string item in tracks)
            {
                if (item.ContainsAll(args))
                {
                    _audioManager.Play(item);
                }
            }
            MessageExtensions.WriteLine("Playing. Type \"info\" for details.");
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
