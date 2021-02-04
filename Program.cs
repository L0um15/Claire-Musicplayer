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
            while (true)
            {
                string input = MessageExtensions.ReadLine();
                MessageExtensions.Flush();
                commandHandler.Parse(input);
            }
        }
    }
}
