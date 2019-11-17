using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;

namespace MinoriEditorShell.Commands
{
    /// <summary>
    /// Wraps a generic ICommandHandler&lt;T&gt; or ICommandListHandler&lt;T&gt;
    /// and allows easy calling of generic interface methods.
    /// </summary>
    public sealed class MesCommandHandlerWrapper
    {
        public static MesCommandHandlerWrapper FromCommandHandler(Type commandHandlerInterfaceType, object commandHandler)
        {
            var updateMethod = commandHandlerInterfaceType.GetMethod("Update");
            var runMethod = commandHandlerInterfaceType.GetMethod("Run");
            return new MesCommandHandlerWrapper(commandHandler, updateMethod, null, runMethod);
        }

        public static MesCommandHandlerWrapper FromCommandListHandler(Type commandHandlerInterfaceType, object commandListHandler)
        {
            var populateMethod = commandHandlerInterfaceType.GetMethod("Populate");
            var runMethod = commandHandlerInterfaceType.GetMethod("Run");
            return new MesCommandHandlerWrapper(commandListHandler, null, populateMethod, runMethod);
        }

        private readonly object _commandHandler;
        private readonly MethodInfo _updateMethod;
        private readonly MethodInfo _populateMethod;
        private readonly MethodInfo _runMethod;

        private MesCommandHandlerWrapper(
            object commandHandler,
            MethodInfo updateMethod, 
            MethodInfo populateMethod,
            MethodInfo runMethod)
        {
            _commandHandler = commandHandler;
            _updateMethod = updateMethod;
            _populateMethod = populateMethod;
            _runMethod = runMethod;
        }

        public void Update(MesCommand command)
        {
            if (_updateMethod != null)
                _updateMethod.Invoke(_commandHandler, new object[] { command });
        }

        public void Populate(MesCommand command, List<MesCommand> commands)
        {
            if (_populateMethod == null)
                throw new InvalidOperationException("Populate can only be called for list-type commands.");
            _populateMethod.Invoke(_commandHandler, new object[] { command, commands });
        }

        public Task Run(MesCommand command) => (Task)_runMethod.Invoke(_commandHandler, new object[] { command });
    }
}
