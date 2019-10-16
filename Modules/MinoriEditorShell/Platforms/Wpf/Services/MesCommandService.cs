using MinoriEditorShell.Commands;
using MinoriEditorShell.Platforms.Wpf.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;

namespace MinoriEditorShell.Platforms.Wpf.Services
{
    [Export(typeof(IMesCommandService))]
    public class MesCommandService : IMesCommandService
    {
        private readonly Dictionary<Type, MesCommandDefinitionBase> _commandDefinitionsLookup;
        private readonly Dictionary<MesCommandDefinitionBase, MesCommand> _commands;
        private readonly Dictionary<MesCommand, MesTargetableCommand> _targetableCommands;

#pragma warning disable 649
        [ImportMany]
        private MesCommandDefinitionBase[] _commandDefinitions;
#pragma warning restore 649

        public MesCommandService()
        {
            _commandDefinitionsLookup = new Dictionary<Type, MesCommandDefinitionBase>();
            _commands = new Dictionary<MesCommandDefinitionBase, MesCommand>();
            _targetableCommands = new Dictionary<MesCommand, MesTargetableCommand>();
        }

        public MesCommandDefinitionBase GetCommandDefinition(Type commandDefinitionType)
        {
            MesCommandDefinitionBase commandDefinition;
            if (!_commandDefinitionsLookup.TryGetValue(commandDefinitionType, out commandDefinition))
                commandDefinition = _commandDefinitionsLookup[commandDefinitionType] =
                    _commandDefinitions.First(x => x.GetType() == commandDefinitionType);
            return commandDefinition;
        }

        public MesCommand GetCommand(MesCommandDefinitionBase commandDefinition)
        {
            MesCommand command;
            if (!_commands.TryGetValue(commandDefinition, out command))
                command = _commands[commandDefinition] = new MesCommand(commandDefinition);
            return command;
        }

        public MesTargetableCommand GetTargetableCommand(MesCommand command)
        {
            MesTargetableCommand targetableCommand;
            if (!_targetableCommands.TryGetValue(command, out targetableCommand))
                targetableCommand = _targetableCommands[command] = new MesTargetableCommand(command);
            return targetableCommand;
        }
    }
}
