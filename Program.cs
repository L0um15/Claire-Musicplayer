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

            Console.WriteLine($"Claire Musicplayer v{Utilities.GetVersion()}");
            Console.WriteLine($"Looking for updates");

            if(Utilities.GetLatestVersion() != null)
                Console.WriteLine($"Claire got an update! v{Utilities.GetLatestVersion()}");

            Console.WriteLine("I am ready");
            while (true)
            {
                string input = MessageExtensions.ReadLine();
                commandHandler.Parse(input);
            }
        }
    }
}
