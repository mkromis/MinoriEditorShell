using System;
using System.ComponentModel.Composition;
using System.Windows;
using System.Windows.Input;
//using System.Windows.Interactivity;

namespace MinoriEditorShell.Platforms.Wpf.Services
{
//	[Export(typeof(IMesInputManager))]
//	public class MesInputManager : IMesInputManager
//	{
//		public void SetShortcut(DependencyObject view, InputGesture gesture, object handler)
//		{
//			var inputBindingTrigger = new MesInputBindingTrigger();
//			inputBindingTrigger.InputBinding = new InputBinding(new RoutedCommand(), gesture);

//			//var target = ViewLocator.LocateForModel(handler, null, null);
//			Interaction.GetTriggers(view).Add(inputBindingTrigger);

//			inputBindingTrigger.Actions.Add(new TestTriggerAction(handler));
//		}

//		public void SetShortcut(InputGesture gesture, object handler)
//		{
//			SetShortcut(Application.Current.MainWindow, gesture, handler);
//		}

//		private class TestTriggerAction : TriggerAction<FrameworkElement>
//		{
//			private readonly object _handler;

//			public TestTriggerAction(object handler)
//			{
//				_handler = handler;
//			}

//			protected override void Invoke(object parameter)
//			{
//                throw new NotImplementedException();
//#warning Invoke
//#if false
//                var context = new ActionExecutionContext
//                {
//                    Target = _handler,
//                    Message = new ActionMessage { MethodName = "Execute" },
//                    Method = _handler.GetType().GetMethod("Execute")
//                };
//			    ActionMessage.PrepareContext(context);

//                if (context.CanExecute())
//				    ActionMessage.InvokeAction(context);
//#endif
//			}
//		}
//	}
}
