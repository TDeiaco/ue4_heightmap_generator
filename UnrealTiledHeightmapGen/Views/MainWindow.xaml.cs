using System.Windows;
using UnrealTiledHeightmapGen.Core;

namespace UnrealTiledHeightmapGen.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            ResolveAssemblies.SetupAssemblyResolveOnCurrentAppDomain();

            InitializeComponent();

            DataContext = new MainWindowDataContext();
        }
        
    }
}
