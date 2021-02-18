using Claire_Musicplayer.Interfaces;
using Claire_Musicplayer.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Claire_Musicplayer.Commands.Audio
{
    public class ShuffleCommand : ICommander
    {
        public void Execute(ReadOnlySpan<string> args)
        {

            if(DirectoryHelper.Tracklist != null)
            {
                // Shuffles playlist using Fisher-Yates Algorithm
                List<string> arr = new List<string>(DirectoryHelper.Tracklist);

                for(int i = arr.Count - 1; i > 0; i--)
                {
                    int rand = new Random().Next(i);
                    string temp = arr[i];
                    arr[i] = arr[rand];
                    arr[rand] = temp;
                }
                DirectoryHelper.Tracklist.Clear();
                DirectoryHelper.Tracklist.AddRange(arr);
                MessageExtensions.WriteLine("Shuffled Tracklist!");
            }

        }

        public string[] GetMan()
        {
            throw new NotImplementedException();
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
