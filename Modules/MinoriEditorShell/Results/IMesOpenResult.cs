using System;

namespace MinoriEditorShell.Results
{
#warning IResult
    /// <summary>
    /// Supposed to be for a windows that pops up with results when done
    /// </summary>
    /// <typeparam name="TChild"></typeparam>
    public interface IMesOpenResult<TChild> /*: IResult*/
    {
        /// <summary>
        /// When configuring
        /// </summary>
        Action<TChild> OnConfigure { get; set; }
        /// <summary>
        /// Action when done
        /// </summary>
        Action<TChild> OnShutDown { get; set; }

        /// <summary>
        /// When results need to be set
        /// </summary>
        //void SetData<TData>(TData data);
    }
}