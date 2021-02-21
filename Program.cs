using Claire_Musicplayer.Services;
using Claire_Musicplayer.Services.Audio;
using System;

namespace Claire_Musicplayer
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            AudioManager audioHandler = new AudioManager();
            CommandHandler commandHandler = new CommandHandler(audioHandler);

            DirectoryHelper.RefreshTracklist();

            while (true)
            {
                string input = MessageExtensions.ReadLine();
                commandHandler.Parse(input);
            }
        }
    }
}
