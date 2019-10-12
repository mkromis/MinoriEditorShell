using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using MinoriEditorShell.Commands;
using MvvmCross;

namespace MinoriEditorShell.Platforms.Wpf.Commands
{
    [Export(typeof(ICommandRouter))]
    public class CommandRouter : ICommandRouter
    {
        private static readonly Type CommandHandlerInterfaceType = typeof(ICommandHandler<>);
        private static readonly Type CommandListHandlerInterfaceType = typeof(ICommandListHandler<>);

        private readonly Dictionary<Type, CommandHandlerWrapper> _globalCommandHandlerWrappers;
        private readonly Dictionary<Type, HashSet<Type>> _commandHandlerTypeToCommandDefinitionTypesLookup;
            
        [ImportingConstructor]
        public CommandRouter(
            [ImportMany(typeof(ICommandHandler))] ICommandHandler[] globalCommandHandlers)
        {
            _commandHandlerTypeToCommandDefinitionTypesLookup = new Dictionary<Type, HashSet<Type>>();
            _globalCommandHandlerWrappers = BuildCommandHandlerWrappers(globalCommandHandlers);
        }

        private Dictionary<Type, CommandHandlerWrapper> BuildCommandHandlerWrappers(ICommandHandler[] commandHandlers)
        {
            List<ICommandHandler> commandHandlersList = SortCommandHandlers(commandHandlers);

            // Command handlers are either ICommandHandler<T> or ICommandListHandler<T>.
            // We need to extract T, and use it as the key in our dictionary.

            Dictionary<Type, CommandHandlerWrapper> result = new Dictionary<Type, CommandHandlerWrapper>();

            foreach (ICommandHandler commandHandler in commandHandlersList)
            {
                Type commandHandlerType = commandHandler.GetType();
                EnsureCommandHandlerTypeToCommandDefinitionTypesPopulated(commandHandlerType);
                HashSet<Type> commandDefinitionTypes = _commandHandlerTypeToCommandDefinitionTypesLookup[commandHandlerType];
                foreach (Type commandDefinitionType in commandDefinitionTypes)
                {
                    result[commandDefinitionType] = CreateCommandHandlerWrapper(commandDefinitionType, commandHandler);
                }
            }

            return result;
        }

        private static List<ICommandHandler> SortCommandHandlers(ICommandHandler[] commandHandlers)
        {
            // Put command handlers defined in priority assemblies, last. This allows applications
            // to override built-in command handlers.
#warning SortCommandHandelrs
#if false
            var bootstrapper = IoC.Get<AppBootstrapper>();

            return commandHandlers
                .OrderBy(h => bootstrapper.PriorityAssemblies.Contains(h.GetType().Assembly) ? 1 : 0)
                .ToList();
#endif
            throw new NotImplementedException();
        }

        public CommandHandlerWrapper GetCommandHandler(CommandDefinitionBase commandDefinition)
        {
#warning GetCommandHandler
#if false
            CommandHandlerWrapper commandHandler;

            var shell = Mvx.IoCProvider.Resolve<IShell>();

            var activeItemViewModel = shell.ActiveLayoutItem;
            if (activeItemViewModel != null)
            {
                commandHandler = GetCommandHandlerForLayoutItem(commandDefinition, activeItemViewModel);
                if (commandHandler != null)
                    return commandHandler;
            }

            var activeDocumentViewModel = shell.ActiveItem;
            if (activeDocumentViewModel != null && !Equals(activeDocumentViewModel, activeItemViewModel))
            {
                commandHandler = GetCommandHandlerForLayoutItem(commandDefinition, activeDocumentViewModel);
                if (commandHandler != null)
                    return commandHandler;
            }

            // If none of the objects in the DataContext hierarchy handle the command,
            // fallback to the global handler.
            if (!_globalCommandHandlerWrappers.TryGetValue(commandDefinition.GetType(), out commandHandler))
                return null;

            return commandHandler;
#endif
            throw new NotImplementedException();
        }

        private CommandHandlerWrapper GetCommandHandlerForLayoutItem(CommandDefinitionBase commandDefinition, object activeItemViewModel)
        {
#warning GetCommandHendlerForLayoutItem
#if false
            var activeItemView = ViewLocator.LocateForModel(activeItemViewModel, null, null);
            var activeItemWindow = Window.GetWindow(activeItemView);
            if (activeItemWindow == null)
                return null;

            var startElement = FocusManager.GetFocusedElement(activeItemView) ?? activeItemView;

            // First, we look at the currently focused element, and iterate up through
            // the tree, giving each DataContext a chance to handle the command.
            return FindCommandHandlerInVisualTree(commandDefinition, startElement);
#endif
            throw new NotImplementedException();
        }

        private CommandHandlerWrapper FindCommandHandlerInVisualTree(CommandDefinitionBase commandDefinition, IInputElement target)
        {
            if (!(target is DependencyObject visualObject))
            {
                return null;
            }

            Object previousDataContext = null;
            do
            {
                if (visualObject is FrameworkElement frameworkElement)
                {
                    Object dataContext = frameworkElement.DataContext;
                    if (dataContext != null && !ReferenceEquals(dataContext, previousDataContext))
                    {
                        if (dataContext is ICommandRerouter commandRerouter)
                        {
                            Object commandTarget = commandRerouter.GetHandler(commandDefinition);
                            if (commandTarget != null)
                            {
                                if (IsCommandHandlerForCommandDefinitionType(commandTarget, commandDefinition.GetType()))
                                {
                                    return CreateCommandHandlerWrapper(commandDefinition.GetType(), commandTarget);
                                }

                                throw new InvalidOperationException("This object does not handle the specified command definition.");
                            }
                        }

                        if (IsCommandHandlerForCommandDefinitionType(dataContext, commandDefinition.GetType()))
                        {
                            return CreateCommandHandlerWrapper(commandDefinition.GetType(), dataContext);
                        }

                        previousDataContext = dataContext;
                    }
                }
                visualObject = VisualTreeHelper.GetParent(visualObject);
            } while (visualObject != null);

            return null;
        }

        private static CommandHandlerWrapper CreateCommandHandlerWrapper(Type commandDefinitionType, Object commandHandler)
        {
            if (typeof(CommandDefinition).IsAssignableFrom(commandDefinitionType))
            {
                return CommandHandlerWrapper.FromCommandHandler(CommandHandlerInterfaceType.MakeGenericType(commandDefinitionType), commandHandler);
            }

            if (typeof(CommandListDefinition).IsAssignableFrom(commandDefinitionType))
            {
                return CommandHandlerWrapper.FromCommandListHandler(CommandListHandlerInterfaceType.MakeGenericType(commandDefinitionType), commandHandler);
            }

            throw new InvalidOperationException();
        }

        private Boolean IsCommandHandlerForCommandDefinitionType(
            Object commandHandler, Type commandDefinitionType)
        {
            Type commandHandlerType = commandHandler.GetType();
            EnsureCommandHandlerTypeToCommandDefinitionTypesPopulated(commandHandlerType);
            HashSet<Type> commandDefinitionTypes = _commandHandlerTypeToCommandDefinitionTypesLookup[commandHandlerType];
            return commandDefinitionTypes.Contains(commandDefinitionType);
        }

        private void EnsureCommandHandlerTypeToCommandDefinitionTypesPopulated(Type commandHandlerType)
        {
            if (!_commandHandlerTypeToCommandDefinitionTypesLookup.ContainsKey(commandHandlerType))
            {
                HashSet<Type> commandDefinitionTypes = _commandHandlerTypeToCommandDefinitionTypesLookup[commandHandlerType] = new HashSet<Type>();

                foreach (Type handledCommandDefinitionType in GetAllHandledCommandedDefinitionTypes(commandHandlerType, CommandHandlerInterfaceType))
                {
                    commandDefinitionTypes.Add(handledCommandDefinitionType);
                }

                foreach (Type handledCommandDefinitionType in GetAllHandledCommandedDefinitionTypes(commandHandlerType, CommandListHandlerInterfaceType))
                {
                    commandDefinitionTypes.Add(handledCommandDefinitionType);
                }
            }
        }

        private static IEnumerable<Type> GetAllHandledCommandedDefinitionTypes(
            Type type, Type genericInterfaceType)
        {
            List<Type> result = new List<Type>();

            while (type != null)
            {
                result.AddRange(type.GetInterfaces()
                    .Where(x => x.IsGenericType && x.GetGenericTypeDefinition() == genericInterfaceType)
                    .Select(x => x.GetGenericArguments().First()));

                type = type.BaseType;
            }

            return result;
        }
    }
}
