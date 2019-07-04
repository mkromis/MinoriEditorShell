using System;

namespace MinoriEditorStudio.Results
{
#warning IResult
    public interface IOpenResult<TChild> /*: IResult*/
	{
		Action<TChild> OnConfigure { get; set; }
		Action<TChild> OnShutDown { get; set; }

		//void SetData<TData>(TData data);
	}
}
