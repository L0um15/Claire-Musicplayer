using Claire_Musicplayer.Interfaces;
using Claire_Musicplayer.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Claire_Musicplayer.Commands.Audio
{
    public class PlayCommand : ICommander
    {
        private readonly AudioHandler _audioHandler;

        public PlayCommand(AudioHandler audioHandler)
        {
            _audioHandler = audioHandler;
        }
        public void Execute(ReadOnlySpan<string> args)
        {
            if (args.Length == 0)
            {
                _audioHandler.Play();
                return;
            }

            if (args[0] == string.Empty)
                return;

            List<string> tracks = Directory.GetFiles(DirectoryHelper.CurrentDirectory, "*", SearchOption.TopDirectoryOnly).ToList();

            foreach(string item in tracks)
            {
                if (item.ContainsAll(args))
                {
                    _audioHandler.Play(item);
                }
            }
            MessageExtensions.WriteLine("Playing. Type \"info\" for details.");
        }

        public string Help()
        {
            return "Play's track";
        }

        public string GetName()
        {
            return "play";
        }
    }
}
