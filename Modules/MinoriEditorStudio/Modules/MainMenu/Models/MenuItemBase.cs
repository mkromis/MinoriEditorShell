using System.Collections;
using System.Collections.Generic;
using MvvmCross.ViewModels;

namespace MinoriEditorStudio.Modules.MainMenu.Models
{
    public class MenuItemBase : MvxNotifyPropertyChanged, IEnumerable<MenuItemBase>
    {
        #region Static stuff

        public static MenuItemBase Separator => new MenuItemSeparator();

        #endregion

        #region Properties

        public MvxObservableCollection<MenuItemBase> Children { get; }

        #endregion

        #region Constructors

        protected MenuItemBase()
        {
            Children = new MvxObservableCollection<MenuItemBase>();
        }

        #endregion

        public void Add(params MenuItemBase[] menuItems)
        {
            Children.AddRange(menuItems);
        }

        public IEnumerator<MenuItemBase> GetEnumerator() => Children.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
