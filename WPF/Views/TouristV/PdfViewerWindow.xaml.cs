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
using System.Windows.Shapes;

namespace BookingApp.WPF.Views.TouristV
{
    /// <summary>
    /// Interaction logic for PdfViewerWindow.xaml
    /// </summary>
    public partial class PdfViewerWindow : Window
    {
        public PdfViewerWindow(string pdfFilePath)
        {
            InitializeComponent();
            pdfWebViewer.Navigate(new Uri(pdfFilePath));
        }
    }
}
