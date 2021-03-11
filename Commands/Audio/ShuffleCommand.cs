using Claire.Interfaces;
using Claire.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Claire.Commands.Audio
{
    public class ShuffleCommand : ICommander
    {
        public void Execute(ReadOnlySpan<string> args)
        {

            if(DirectoryHelper.Tracklist != null)
            {
                // Shuffles playlist using Fisher-Yates Algorithm
                List<string> arr = DirectoryHelper.Tracklist;
                var random = new Random();
                for(int i = arr.Count - 1; i > 0; i--)
                {
                    int rand = random.Next(i);
                    string temp = arr[i];
                    arr[i] = arr[rand];
                    arr[rand] = temp;
                }
                Console.WriteLine("Shuffled Tracklist!");
            }

        }

        public string[] GetManual()
        {
            return new string[] {
                $"{GetName()} - {Help()}",
                "Needs: Nothing",
                $"Usage: {GetName()}",
                "Description: Shuffles playlist using Fisher-Yates Algorithm"
            };
        }

        public string GetName()
        {
            return "shuffle";
        }

        public string Help()
        {
            return "Shuffles playlist";
        }
    }
}
