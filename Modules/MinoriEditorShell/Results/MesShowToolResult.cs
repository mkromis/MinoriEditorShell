using System;
using System.ComponentModel.Composition;
using MinoriEditorShell.Services;

namespace MinoriEditorShell.Results
{
    public class MesShowToolResult<TTool> : MesOpenResultBase<TTool>
		where TTool : IMesTool
	{
#warning ToolLocator
        private readonly Func<TTool> _toolLocator = null; //() => Mvx.IoCProvider.Resolve<TTool>();

#pragma warning disable 649
        [Import]
		private IMesManager _shell;
#pragma warning restore 649

        public MesShowToolResult()
		{
			
		}

		public MesShowToolResult(TTool tool)
		{
			_toolLocator = () => tool;
		}

#warning CoroutineExecutionContext
        public override void Execute(/*CoroutineExecutionContext*/Object context)
		{
            throw new NotImplementedException();
#if false
            var tool = _toolLocator();

			if (_setData != null)
				_setData(tool);

			if (_onConfigure != null)
				_onConfigure(tool);

			tool.Deactivated += (s, e) =>
			{
				if (!e.WasClosed)
					return;

				if (_onShutDown != null)
					_onShutDown(tool);

				OnCompleted(null, false);
			};

			_shell.ShowTool(tool);
#endif
		}
	}
}
