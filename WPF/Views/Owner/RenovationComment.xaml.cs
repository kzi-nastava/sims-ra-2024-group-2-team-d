using BookingApp.Domain.Model;
using BookingApp.Services;
using BookingApp.ViewModel;
using InitialProject.CustomClasses;
using InitialProject.Presentation.WPF.ViewModel.Owner;
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

namespace BookingApp.View.Owner
{
    /// <summary>
    /// Interaction logic for RenovationComment.xaml
    /// </summary>
    public partial class RenovationComment : Window
    {
        public BaseService baseService { get; set; }
        public DateRange range;
        public Accommodation accomo;
        public User _user;
        public RenovationComment(DateRange range,Accommodation acc,User user)
        {
            InitializeComponent();
            baseService = BaseService.getInstance();
            this.range = range;
            accomo = acc;
            _user = user;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Renovation r = new Renovation();
            r.RenovationDateRange = range;
            r.Comment = CommentBox.Text;
            r.AccomodationId = accomo.Id;
            r.UserId = _user.Id;
            baseService.RenovationService.Save(r);


            MessageBox.Show("Uspjesno ste zakazali renoviranje");


        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
