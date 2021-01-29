using Claire_Musicplayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Claire_Musicplayer.Commands
{
    public class Test : ICommander
    {
        public void Execute(string[] args)
        {
            MessageExtensions.WriteLine("Test ran Successfully");
        }

        public string Help()
        {
            return "simple test";
        }

        public string Invoke()
        {
            return "test";
        }
    }
}
