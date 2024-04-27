using BookingApp.Dto;
using BookingApp.Model;
using BookingApp.Repository;
using System;
using System.Collections.Generic;
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
        public User LoggedInUser { get; set; }
        public TourRequest TourRequest { get; set; }
        public TourRequestRepository _tourRequestRepository { get; set; }
        public TourInstanceRepository _tourInstanceRepository { get; set; }

        public TourInstance TourInstance { get; set; }
        public TourDto TourDto { get; set; }

        //public string Date { get; set; }

        public AcceptReqGuide(TourRequest tourRequest, User user)
        {
            InitializeComponent();
            LoggedInUser = user;
            _tourRequestRepository = new TourRequestRepository();
            _tourInstanceRepository = new TourInstanceRepository();
            TourRequest = tourRequest;
            TourDto = new TourDto();


        }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            if (DateTextBox.Text == "")
            {
                MessageBox.Show("You have to set date!");

            }
            else
            {
                string format = "yyyy-MM-dd HH:mm";
                DateTime parsedDate;
                if (DateTime.TryParseExact(DateTextBox.Text, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out parsedDate))
                {
                    DateTime StartDateTime = new DateTime(TourRequest.Start.Year, TourRequest.Start.Month, TourRequest.Start.Day);
                    DateTime EndDateTime = new DateTime(TourRequest.End.Year, TourRequest.End.Month, TourRequest.End.Day);

                    if (parsedDate.Date >= StartDateTime && parsedDate.Date <= EndDateTime && _tourInstanceRepository.CheckIfUserIsAvaliable(LoggedInUser, parsedDate))
                    {
                        TourRequest.CurrentStatus = Status.Accepted;
                        _tourRequestRepository.Update(TourRequest);
                        CreateTourInstanceFromRequest(TourRequest, parsedDate);

                        MessageBox.Show("You have successfully acceptted this request!");
                        this.Close();
                    }

                    else
                    {
                        MessageBox.Show("You have to enter date in valid date range");
                    }
                }
                else
                {
                    MessageBox.Show("You have to enter date in format yyyy-MM-dd HH:mm");
                }

            }




            /* string validTour = TourDto.IsValid;

             if (validTour == string.Empty)
             {
                 string format = "dd/MM/yyyy HH:mm";
                 DateTime parsedDate;
                 DateTime.TryParseExact(TourDto.Dates, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out parsedDate);
                 DateTime StartDateTime = new DateTime(TourRequest.Start.Year, TourRequest.Start.Month, TourRequest.Start.Day);
                 DateTime EndDateTime = new DateTime(TourRequest.End.Year, TourRequest.End.Month, TourRequest.End.Day);

                 if (parsedDate.Date >= StartDateTime && parsedDate.Date <= EndDateTime /*&& CheckIfUserIsAvaliable(user, parsedDate)*//*)
                 {
                     TourRequest.CurrentStatus = Status.Accepted;
                     _tourRequestRepository.Update(TourRequest);
                     CreateTourInstanceFromRequest(TourRequest, parsedDate);

                     MessageBox.Show("You have successfully acceptted this request!");
                     this.Close();
                 }*/




            /*TourRequests.Remove(selectedRequest);

            RequestsGrid.ItemsSource = TourRequests;
            RequestsGrid.DataContext = TourRequests;*/
        }



        public void CreateTourInstanceFromRequest(TourRequest request, DateTime dateTime)
        {
            TourInstance = new TourInstance(request.Id, dateTime);
            _tourInstanceRepository.Save(TourInstance);
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }


        
    }
}
