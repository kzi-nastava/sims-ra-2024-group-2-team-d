using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using BookingApp.Model;
using BookingApp.Repository;
using BookingApp.Services;
using BookingApp.View.Owner;
using BookingApp.ViewModel.Guide;

namespace BookingApp.ViewModel
{
    public class RenovationRecommendationViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        public BaseService baseService { get; set; }

        public Reservation reservation { get; set; }

        public MyCommand Cancel { get; set; }

        public MyCommand Finish { get; set; }

        public string Condition { get; set; }
        public int RenovationUrgency { get; set; }


        private bool _selectedRenovationLevel;
        public bool SelectedRenovationLevel
        {
            get { return _selectedRenovationLevel; }
            set
            {
                if (_selectedRenovationLevel != value)
                {
                    _selectedRenovationLevel = value;
                    OnPropertyChanged(nameof(SelectedRenovationLevel));
                }
            }
        }

        private bool _selectedRenovationLevel1;
        public bool SelectedRenovationLevel1
        {
            get { return _selectedRenovationLevel1; }
            set
            {
                if (_selectedRenovationLevel1 != value)
                {
                    _selectedRenovationLevel1 = value;
                    OnPropertyChanged(nameof(SelectedRenovationLevel1));
                }
            }
        }


        private bool _selectedRenovationLevel2;
        public bool SelectedRenovationLevel2
        {
            get { return _selectedRenovationLevel2; }
            set
            {
                if (_selectedRenovationLevel2 != value)
                {
                    _selectedRenovationLevel2 = value;
                    OnPropertyChanged(nameof(SelectedRenovationLevel2));
                }
            }
        }

        private bool _selectedRenovationLevel3;
        public bool SelectedRenovationLevel3
        {
            get { return _selectedRenovationLevel3; }
            set
            {
                if (_selectedRenovationLevel3 != value)
                {
                    _selectedRenovationLevel3 = value;
                    OnPropertyChanged(nameof(SelectedRenovationLevel3));
                }
            }
        }

        private bool _selectedRenovationLevel4;
        public bool SelectedRenovationLevel4
        {
            get { return _selectedRenovationLevel4; }
            set
            {
                if (_selectedRenovationLevel4 != value)
                {
                    _selectedRenovationLevel4 = value;
                    OnPropertyChanged(nameof(SelectedRenovationLevel4));
                }
            }
        }


        public RenovationRecommendationViewModel(Reservation reservation)
        {
            this.reservation = reservation;
            this.baseService = BaseService.getInstance();
            Cancel = new MyCommand(Cancel_click);
            Finish = new MyCommand(Finish_click);
            SelectedRenovationLevel = false;
        }

        public void Cancel_click()
        {

        }

        public void Finish_click()
        {
            int level = -1;

            if (SelectedRenovationLevel == true)
            {
                level = 1;
            }

            if (SelectedRenovationLevel1 == true)
            {
                level = 2;
            }

            if (SelectedRenovationLevel2 == true)
            {
                level = 3;
            }

            if (SelectedRenovationLevel3 == true)
            {
                level = 4;
            }

            if (SelectedRenovationLevel4 == true)
            {
                level = 5;
            }


            RenovationRecommendation renovationRecommendation = new RenovationRecommendation(reservation.Id, reservation.AccomodationId, reservation.UserId, Condition, level);
            baseService.RenovationRecommendationService.Save(renovationRecommendation);
            MessageBox.Show("Uspjesno ste napisali preporuku");

        }


        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
