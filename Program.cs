using Claire_Musicplayer.Services;
using Claire_Musicplayer.Services.Audio;
using System;

namespace Claire_Musicplayer
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            Console.SetBufferSize(Console.WindowWidth, Console.WindowHeight);
            AudioManager audioHandler = new AudioManager();
            CommandHandler commandHandler = new CommandHandler(audioHandler);


            // This should be only used during startup and cd command 
            DirectoryHelper.PrepareTracklist();

            MessageExtensions.WriteLine($"Claire Musicplayer v{Utilities.GetVersion()}");
            MessageExtensions.WriteLine($"Looking for updates");

            if(Utilities.GetLatestVersion() != null)
                MessageExtensions.WriteLine($"Claire got an update! v{Utilities.GetLatestVersion()}");

            MessageExtensions.WriteLine("I am ready");
            while (true)
            {
                string input = MessageExtensions.ReadLine();
                MessageExtensions.Flush();
                commandHandler.Parse(input);
            }
        }
    }
}
