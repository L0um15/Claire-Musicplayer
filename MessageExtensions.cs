using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Claire_Musicplayer
{
    public static class MessageExtensions
    {
        private static readonly List<object> _outputHistory = new List<object>();

        /// <summary>
        /// Writes text to console with padding equal to 3
        /// </summary>
        /// <param name="message"></param>
        public static void WriteLine(object message)
        {
            if (Console.CursorTop == Console.BufferHeight - 3)
            {
                var intended = _outputHistory.Skip(1).Take(_outputHistory.Count).ToList();
                _outputHistory.Clear();
                intended.ForEach(x => { _outputHistory.Add(x); });
            }
            Console.Clear();
            _outputHistory.Add(message);
            for (int i = 0; i < _outputHistory.Count; i++)
            {
                Console.WriteLine(_outputHistory[i]);
            }
        }
        /// <summary>
        /// Moves cursor to the bottom of the console and waits for user input
        /// </summary>
        /// <returns></returns>
        public static string ReadLine()
        {
            int defPosY = Console.CursorTop;
            int defPosX = Console.CursorLeft;

            Console.CursorTop = Console.BufferHeight - 1;
            Console.CursorLeft = 0;

            Console.Write(":");

            string input = Console.ReadLine();

            Console.CursorTop = defPosY;
            Console.CursorLeft = defPosX;

            return input;
        }
        public static void Refresh()
        {
            Console.Clear();
            for (int i = 0; i < _outputHistory.Count; i++)
            {
                Console.WriteLine(_outputHistory[i]);
            }
        }
    }
}
