using System.Collections.ObjectModel;

namespace QS_Takeoff.UI.ViewModels
{
    /// <summary>
    /// View model backing the ProjectExplorer control.
    /// </summary>
    public class ProjectExplorerViewModel
    {
        /// <summary>
        /// Collection of drawing names displayed in the explorer.
        /// </summary>
        public ObservableCollection<string> Drawings { get; } = new ObservableCollection<string>();

        /// <summary>
        /// Collection of dimension group names that have been added by the user.
        /// </summary>
        public ObservableCollection<string> DimensionGroups { get; } = new ObservableCollection<string>();
    }
}
