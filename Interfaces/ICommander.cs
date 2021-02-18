using System;

namespace Claire_Musicplayer.Interfaces
{
    public interface ICommander
    {
        public string[] GetManual();
        public string Help();
        public string GetName();
        public void Execute(ReadOnlySpan<string> args);
    }
}
