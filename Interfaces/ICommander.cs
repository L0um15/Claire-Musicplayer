using System;

namespace Claire_Musicplayer.Interfaces
{
    public interface ICommander
    {
        public string Help();
        public string Invoke();
        public void Execute(string[] args);
    }
}
