using CommandDesignPattern.Command;
using System.Collections.Generic;
using System.Windows.Input;

namespace CommandDesignPattern.Invoker
{
    public class ModifyPrice
    {
        private readonly List<ICommandMine> _commands;
        private ICommandMine _command;

        public ModifyPrice() => _commands = new List<ICommandMine>();

        public void SetCommand(ICommandMine command) => _command = command;

        public void Execute()
        {
            _commands.Add(_command);
            _command.ExecuteAction();
        }
    }
}