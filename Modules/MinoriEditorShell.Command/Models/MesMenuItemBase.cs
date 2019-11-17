using System.Collections;
using System.Collections.Generic;
using MvvmCross.ViewModels;

namespace MinoriEditorShell.Models
{
    public class MesMenuItemBase : MvxNotifyPropertyChanged, IEnumerable<MesMenuItemBase>
    {
        #region Static stuff

        public static MesMenuItemBase Separator => new MesMenuItemSeparator();

        #endregion

        #region Properties

        public MvxObservableCollection<MesMenuItemBase> Children { get; }

        #endregion

        #region Constructors

        protected MesMenuItemBase()
        {
            Children = new MvxObservableCollection<MesMenuItemBase>();
        }

        #endregion

        public void Add(params MesMenuItemBase[] menuItems)
        {
            Children.AddRange(menuItems);
        }

        public IEnumerator<MesMenuItemBase> GetEnumerator() => Children.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
