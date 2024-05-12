using BookingApp.Domain.Model;
using BookingApp.Dto;
using BookingApp.Repository;
using BookingApp.Services;
using BookingApp.WPF.ViewModels.Guide;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace BookingApp.View
{
    /// <summary>
    /// Interaction logic for AcceptReqGuide.xaml
    /// </summary>
    public partial class AcceptReqGuide : Window
    {
        //public User LoggedInUser { get; set; }
        //public TourRequest TourRequest { get; set; }
        //public TourRequestRepository _tourRequestRepository { get; set; }
        //public TourInstanceRepository _tourInstanceRepository { get; set; }
        //public TourRepository _tourRepository { get; set; }

        //public List<TourInstance> TourInstances { get; set; }
        //public TourInstance TourInstance { get; set; }
        //public TourDto TourDto { get; set; }

        ////public string Date { get; set; }

        public AcceptReqGuide(TourRequest tourRequest, User user)
        {
            InitializeComponent();
            DataContext = new AcceptReqGuideViewModel(tourRequest, user);
            //LoggedInUser = user;
            //_tourRequestRepository = new TourRequestRepository();
            //_tourInstanceRepository = new TourInstanceRepository();
            //_tourRepository = new TourRepository();
            ////TourInstances = new List<TourInstance>(_tourInstanceRepository.GetAll());
            ////LinkTourInstancesWithTours();
            ////TourInstances = new ObservableCollection<TourInstance>(MainService.TourInstanceService.GetForTheDay1(LoggedInUser, TourInstances));

            //TourRequest = tourRequest;
            ////TourDto = new TourDto();
        }

        /*public void LinkTourInstancesWithTours()
        {
            foreach (TourInstance tourInstance in TourInstances)
            {
                Tour baseTour = _tourRepository.GetById(tourInstance.TourId);
                if (baseTour != null)
                {
                    tourInstance.BaseTour = baseTour;
                }
            }
        }*/

        //private void Ok_Click(object sender, RoutedEventArgs e)
        //{
        //    if (DateTextBox.Text == "")
        //    {
        //        MessageBox.Show("You have to set date!");

        //    }
        //    else
        //    {
        //        string format = "yyyy-MM-dd HH:mm";
        //        DateTime parsedDate;
        //        if (DateTime.TryParseExact(DateTextBox.Text, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out parsedDate))
        //        {
        //            DateTime StartDateTime = new DateTime(TourRequest.Start.Year, TourRequest.Start.Month, TourRequest.Start.Day);
        //            DateTime EndDateTime = new DateTime(TourRequest.End.Year, TourRequest.End.Month, TourRequest.End.Day);

        //            if (parsedDate.Date >= StartDateTime && parsedDate.Date <= EndDateTime 
        //                && _tourInstanceRepository.CheckIfUserIsAvaliable(LoggedInUser, parsedDate, TourInstances) 
        //                && _tourRequestRepository.CheckUserIsAvaliable(LoggedInUser, parsedDate))
        //            {
        //                TourRequest.CurrentStatus = Status.Accepted;
        //                TourRequest.ChosenDateTime = parsedDate;
        //                TourRequest.GuideId = LoggedInUser.Id;
        //                _tourRequestRepository.Update(TourRequest);
        //                //CreateTourInstanceFromRequest(TourRequest, parsedDate);

        //                MessageBox.Show("You have successfully acceptted this request!");
        //                this.Close();
        //            }

        //            else
        //            {
        //                MessageBox.Show("You have to enter date in valid date range");
        //            }
        //        }
        //        else
        //        {
        //            MessageBox.Show("You have to enter date in format yyyy-MM-dd HH:mm");
        //        }

        //    }

        //    /*TourRequests.Remove(selectedRequest);

        //    RequestsGrid.ItemsSource = TourRequests;
        //    RequestsGrid.DataContext = TourRequests;*/
        //}



        ///*public void CreateTourInstanceFromRequest(TourRequest request, DateTime dateTime)
        //{
        //    TourInstance = new TourInstance(request.Id, dateTime);
        //    _tourInstanceRepository.Save(TourInstance);
        //}*/

        //private void Cancel_Click(object sender, RoutedEventArgs e)
        //{
        //    Close();
        //}

    }
}
