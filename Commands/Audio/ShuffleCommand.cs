﻿using Claire_Musicplayer.Interfaces;
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
                List<string> arr = DirectoryHelper.Tracklist;
                var random = new Random();
                for(int i = arr.Count - 1; i > 0; i--)
                {
                    int rand = random.Next(i);
                    string temp = arr[i];
                    arr[i] = arr[rand];
                    arr[rand] = temp;
                }
                MessageExtensions.WriteLine("Shuffled Tracklist!");
            }

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