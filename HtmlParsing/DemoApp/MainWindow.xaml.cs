using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DemoApp
{
    using WhichMan.Utilities.HtmlParsing;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void WindowLoaded(object sender, RoutedEventArgs e)
        {
            const string filePath = "..\\..\\..\\HtmlParsing.Tests\\Data\\sample.htm";
            if (System.IO.File.Exists(filePath))
                LoadFile(filePath);
        }

        private void CloseMe(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void OpenFile(object sender, RoutedEventArgs e)
        {
            // Configure open file dialog box
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.FileName = "";// "Document"; // Default file name
            dlg.DefaultExt = ".txt"; // Default file extension
            dlg.Filter = "HTML documents (.html, .htm)|*.html;*.htm"; // Filter files by extension

            // Show open file dialog box
            bool? result = dlg.ShowDialog();

            // Process open file dialog box results
            if (result != true)
                return;

            LoadFile(dlg.FileName);
        }

        private void LoadFile(string filePath)
        {
            var html = System.IO.File.ReadAllText(filePath);

            var doc = HtmlParser.Parse(html, true);

            var tidy = doc.ToString();

            HtmlSanitizer.Sanitize(doc);
            var sanitized = doc.ToString();

            //fix HTML for display is the XML viewer
            tidy = tidy.Replace("<script type=\"text/javascript\">", "<script type=\"text/javascript\"><![CDATA[")
                .Replace("<script>", "<script><![CDATA[")
                .Replace("</script>", "]]></script>")
                .Replace("&nbsp;", "&#xA0;");

            sanitized = sanitized.Replace("&nbsp;", "&#xA0;");

            viewer1.LoadXml(tidy);
            viewer2.LoadXml(sanitized);
        }
    }
}
