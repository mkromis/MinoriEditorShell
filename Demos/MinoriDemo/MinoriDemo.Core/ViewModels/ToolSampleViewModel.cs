using MinoriEditorStudio.Framework;
using MinoriEditorStudio.Framework.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinoriDemo.Core.Models
{
    class ToolSampleViewModel : Tool
    {
        public override PaneLocation PreferredLocation { get; } = PaneLocation.Right;
        
        public ToolSampleViewModel()
        {
            DisplayName = "Tool Test";
        }
    }
}
