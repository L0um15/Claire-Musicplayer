using Claire_Musicplayer.Interfaces;
using Claire_Musicplayer.Services.Audio;
using System;
using System.Collections.Generic;
using System.Text;

namespace Claire_Musicplayer.Commands.Audio
{
    public class LyricsCommand : ICommander
    {
        private readonly AudioManager _audioManager;

        public LyricsCommand(AudioManager audioManager)
        {
            _audioManager = audioManager;
        }

        public void Execute(ReadOnlySpan<string> args)
        {

            if (_audioManager.CurrentTrack == null)
            {
                MessageExtensions.WriteLine("Currently nothing is playing.");
                return;
            }

            int pageNum = 1;
            if (args.Length == 1)
                if (args[0] != string.Empty)
                    if (int.TryParse(args[0], out int value))
                        pageNum = value;

            string[] lyrics = Utilities.GetTrackInfo(_audioManager.CurrentTrack)
                .Lyrics.Split(new[] { "\r\n", "\n", "\r" }, StringSplitOptions.None);

            if (lyrics != null)
            {
                Paginator<string> paginator = new Paginator<string>(lyrics, Console.BufferHeight - 4);

                if (paginator.IsValidPage(pageNum))
                {
                    Page<string> page = paginator.GetPage(pageNum);

                    for(int i = 0; i < page.Span.Length; i++)
                    {
                        MessageExtensions.WriteLine("   " + page.Span[i]);
                    }
                    MessageExtensions.WriteLine($"PAGE  {page.PageNumber} of {paginator.PageCount}");
                }
                else
                    MessageExtensions.WriteLine("Page was out of range");

            }
            else
                MessageExtensions.WriteLine("Sorry, this track does not contains any lyrics");

        }

        public string GetName()
        {
            return "lyrics";
        }

        public string Help()
        {
            return "Extract's and displays lyrics from audiofile.";
        }
    }
}
