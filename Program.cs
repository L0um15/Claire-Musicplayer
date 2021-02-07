using Claire_Musicplayer.Services;
using System;

namespace Claire_Musicplayer
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            Console.SetBufferSize(Console.WindowWidth, Console.WindowHeight);
            AudioHandler audioHandler = new AudioHandler();
            CommandHandler commandHandler = new CommandHandler(audioHandler);
            MessageExtensions.WriteLine($"Claire Musicplayer v{Utilities.GetVersion()}");
            MessageExtensions.WriteLine($"Looking for updates");
            if(Utilities.GetLatestVersion() != null)
            {
                MessageExtensions.WriteLine($"Claire got an update! v{Utilities.GetLatestVersion()}");
            }
            MessageExtensions.WriteLine("Waiting for orders");
            while (true)
            {
                string input = MessageExtensions.ReadLine();
                MessageExtensions.Flush();
                commandHandler.Parse(input);
            }
        }
    }
}
