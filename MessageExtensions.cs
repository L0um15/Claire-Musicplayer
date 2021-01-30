using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Claire_Musicplayer
{
    public static class MessageExtensions
    {
        private static readonly List<object> _visibleOutput = new List<object>();
        private static readonly List<object> _outputHistory = new List<object>();

        /// <summary>
        /// Takes care of output format and prints to console with bottom padding = 3.
        /// </summary>
        /// <param name="message"></param>
        public static void WriteLine(object message)
        {
            if (Console.CursorTop == Console.BufferHeight - 3)
            {
                var intended = _visibleOutput.Skip(1).Take(_visibleOutput.Count).ToList();
                _visibleOutput.Clear();
                intended.ForEach(x => { _visibleOutput.Add(x); });
            }
            _outputHistory.Add(message);
            _visibleOutput.Add(message);
            Refresh();
        }
        
        /// <summary>
        /// Moves input field to the bottom of console.
        /// </summary>
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
            for (int i = 0; i < _visibleOutput.Count; i++)
            {
                Console.WriteLine(_visibleOutput[i]);
            }
        }
    }
}
