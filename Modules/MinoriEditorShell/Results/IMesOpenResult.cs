using System;

namespace MinoriEditorShell.Results
{
#warning IResult
    public interface IMesOpenResult<TChild> /*: IResult*/
	{
		Action<TChild> OnConfigure { get; set; }
		Action<TChild> OnShutDown { get; set; }

		//void SetData<TData>(TData data);
	}
}
