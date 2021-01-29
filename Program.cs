using System;

namespace Claire_Musicplayer
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            Console.SetBufferSize(Console.WindowWidth, Console.WindowHeight);
            CommandHandler commandHandler = new CommandHandler();
            while (true)
            {
                string input = MessageExtensions.ReadLine();
                commandHandler.Parse(input);
            }
        }
    }
}
