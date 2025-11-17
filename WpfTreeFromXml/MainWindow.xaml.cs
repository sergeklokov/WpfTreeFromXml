using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Xml;

namespace WpfTreeFromXml
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            // BooksData is the resource key for the XmlDataProvider you declared in XAML.
            // It’s basically the name you give to the XML data source so you can reference it later in bindings or code-behind.
            var provider = (XmlDataProvider)FindResource("BooksData");
            provider.DataChanged += Provider_DataChanged;

        }

        // after XML data got loaded into the XmlDataProvider, expand all tree nodes
        private void Provider_DataChanged(object sender, EventArgs e)
        {
            // Dispatcher ensures UI thread execution
            Dispatcher.BeginInvoke(new Action(() =>
            {
                foreach (var item in DemoTreeView.Items)
                {
                    if (item is XmlElement)
                    {
                        var container = DemoTreeView.ItemContainerGenerator.ContainerFromItem(item) as TreeViewItem;
                        container?.ExpandSubtree();
                    }
                }
            }), DispatcherPriority.Background);
        }

    }
}