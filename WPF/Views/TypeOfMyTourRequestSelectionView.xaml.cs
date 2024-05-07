using BookingApp.WPF.ViewModels;
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
using BookingApp.Model;

namespace BookingApp.WPF.Views
{
    /// <summary>
    /// Interaction logic for TypeOfMyTourRequestSelectionView.xaml
    /// </summary>
    public partial class TypeOfMyTourRequestSelectionView : Window
    {
        public TypeOfMyTourRequestSelectionView(User loggedInUser)
        {
            InitializeComponent();
            DataContext = new TypeOfMyTourRequestSelectionViewModel(loggedInUser);
        }
    }
}
