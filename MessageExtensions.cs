using Claire_Musicplayer.Services;
using Pastel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Claire_Musicplayer
{
    public static class MessageExtensions
    {

        private static string USERNAME => Environment.UserName;
        private static string HOSTNAME => Environment.MachineName;
        
        public static string ReadLine()
        {

            var dirprompt = new (string color, string letter)[]
            {
                ("#F568CE","┌["),
                ("#BCE05C",DirectoryHelper.CurrentDirectory),
                ("F568CE","]"),
            };

            var userprompt = new (string color, string letter)[]{
                ("#F568CE", "└["),
                ("#F58600", USERNAME),
                ("#1BAFF5", "@"),
                ("#67F5F2", HOSTNAME + " "),
                ("#F568CE", "]"),
                ("#F1E4E0", "$")
            };

            Console.WriteLine(string.Join("", dirprompt.Select(s=>s.letter.Pastel(s.color))));
            Console.Write(string.Join("", userprompt.Select(s => s.letter.Pastel(s.color))) + " ");

            return Console.ReadLine();
        }

    }
}
