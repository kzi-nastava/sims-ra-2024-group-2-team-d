using BookingApp.Model;
using BookingApp.Repository;
using BookingApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace BookingApp.View.Owner
{
    public partial class AccommodationReviews : Window
    {
       public AccommodationReviews(User user)
        {
            InitializeComponent();
            DataContext = new AccommodationReviewsViewModel(user);
     
        }

        }
    }
