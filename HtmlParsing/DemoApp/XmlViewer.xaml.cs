using System.Windows.Controls;
using System.Windows.Data;

namespace DemoApp
{
    using System.Xml;

    /// <summary>
    /// Interaction logic for XmlViewer.xaml
    /// </summary>
    public partial class XmlViewer : UserControl
    {
        public XmlViewer()
        {
            InitializeComponent();
        }

        public void LoadXml(string xml)
        {
            var xDoc = new XmlDocument();
            xDoc.LoadXml(xml);

            var provider = new XmlDataProvider();
            provider.Document = xDoc;
            Binding binding = new Binding();
            binding.Source = provider;
            binding.XPath = "child::node()";
            XmlTree.SetBinding(TreeView.ItemsSourceProperty, binding);
        }
    }
}
