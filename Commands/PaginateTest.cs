using Claire_Musicplayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Claire_Musicplayer.Commands
{
    public class PaginateTest : ICommander
    {
        public void Execute(string[] args)
        {
            if (args.Length < 1) return;


            if(int.TryParse(args[0], out int value))
            {
                List<int> list = new List<int>();
                for (int i = 0; i < 125; i++)
                {
                    list.Add(i);
                }
                IList<int> page = list.Paginate(value,Console.BufferHeight - 3);
                foreach(var item in page)
                {
                    MessageExtensions.WriteLine(item);
                }
            }      
        }

        public string Help()
        {
            throw new NotImplementedException();
        }

        public string Invoke()
        {
            return "ls";
        }
    }
}
