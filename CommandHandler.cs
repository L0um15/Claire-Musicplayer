using Claire_Musicplayer.Commands.Audio;
using Claire_Musicplayer.Commands.Terminal;
using Claire_Musicplayer.Commands.Movement;
using Claire_Musicplayer.Interfaces;
using Claire_Musicplayer.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Claire_Musicplayer
{
    public class CommandHandler
    {
        private static readonly Dictionary<string, ICommander> _commands = new Dictionary<string, ICommander>();

        public CommandHandler(AudioHandler audioHandler)
        {
            #region AudioCommands
            AddCommand(new PlayCommand(audioHandler));
            AddCommand(new PauseCommand(audioHandler));
            AddCommand(new StopCommand(audioHandler));
            AddCommand(new VolumeCommand(audioHandler));
            AddCommand(new InfoCommand(audioHandler));
            #endregion

            #region MovementCommands
            AddCommand(new ListCommand());
            AddCommand(new ChangeDirCommand());
            #endregion

            #region TerminalCommands
            AddCommand(new ClearConsoleCommand());
            #endregion
        }

        public void AddCommand(ICommander command)
        {
            if (!_commands.ContainsKey(command.Invoke()))
                _commands.Add(command.Invoke(), command);
        }

        public void Parse(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                MessageExtensions.WriteLine("Nothing passed.");
                return;
            }
            string[] args = input.Split(" ").ToArray();
            string command = args[0];
            args = args.Skip(1).Take(args.Length).ToArray();
            if (_commands.ContainsKey(command))
                _commands[command].Execute(args);
            else
                MessageExtensions.WriteLine("Invalid Command, Try Again.");
        }
    }
}
