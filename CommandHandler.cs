using Claire.Commands.Audio;
using Claire.Commands.Terminal;
using Claire.Commands.Movement;
using Claire.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Claire.Services.Audio;

namespace Claire
{
    public class CommandHandler
    {
        public static readonly Dictionary<string, ICommander> commands = new Dictionary<string, ICommander>(StringComparer.InvariantCultureIgnoreCase);

        public CommandHandler(AudioManager audioHandler)
        {
            #region AudioCommands
            AddCommand(new PlayCommand(audioHandler));
            AddCommand(new PauseCommand(audioHandler));
            AddCommand(new StopCommand(audioHandler));
            AddCommand(new SkipCommand(audioHandler));
            AddCommand(new VolumeCommand(audioHandler));
            AddCommand(new InfoCommand(audioHandler));
            AddCommand(new LyricsCommand(audioHandler));
            AddCommand(new ShuffleCommand());
            #endregion

            #region MovementCommands
            AddCommand(new ListCommand());
            AddCommand(new ChangeDirCommand());
            #endregion

            #region TerminalCommands
            AddCommand(new ClearConsoleCommand());
            AddCommand(new HelpCommand());
            AddCommand(new ManCommand());
            AddCommand(new ExitCommand(audioHandler));
            #endregion
        }

        public void AddCommand(ICommander command)
        {
            if (!commands.ContainsKey(command.GetName()))
                commands.Add(command.GetName(), command);
        }

        public void Parse(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                Console.WriteLine("Nothing passed.");
                return;
            }
            string[] args = input.Split(' ');
            string command = args[0];
            var span = args.AsSpan(1);
            if (commands.ContainsKey(command))
                commands[command].Execute(span);
            else
                Console.WriteLine("Invalid Command, Try Again.");
        }
    }
}
