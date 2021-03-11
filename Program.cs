using Claire.Services;
using Claire.Services.Audio;
using Pastel;
using System;

namespace Claire
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            AudioManager audioHandler = new AudioManager();
            CommandHandler commandHandler = new CommandHandler(audioHandler);
            DirectoryHelper.RefreshTracklist();

            string latestversion = Utilities.GetLatestVersion();

            if (latestversion != null)
                Console.WriteLine($"CLAIRE IS OUTDATED! Newest: {latestversion}".Pastel("#F5BE47"));

            while (true)
            {
                string input = MessageExtensions.ReadLine();
                commandHandler.Parse(input);
            }
        }
    }
}
