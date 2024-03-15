using BookingApp.Model;
using BookingApp.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Interaction logic for CreateTour.xaml
    /// </summary>
    public partial class CreateTour : Window //, INotifyPropertyChanged
    {
        public User LoggedInUser { get; set; }

        public Tour SelectedTour { get; set; }

        private readonly TourRepository _repository;
        private readonly KeyPointRepository _keyPointRepository;
        private readonly KeyPoint _keyPoint;

        private string _text;
        public string Text
        {
            get => _text;
            set
            {
                if (value != _text)
                {
                    _text = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _title;
        public string Title
        {
            get => _title;
            set
            {
                if (value != _title)
                {
                    _title = value;
                    OnPropertyChanged("Title");
                }
            }
        }

        private string _description;
        public string Description
        {
            get => _description;
            set
            {
                if (value != _description)
                {
                    _description = value;
                    OnPropertyChanged("Description");
                }
            }
        }

        private string _location;
        public string Location
        {
            get => _location;
            set
            {
                if (value != _location)
                {
                    _location = value;
                    OnPropertyChanged("Location");
                }
            }
        }

        private string _language;
        public string Language
        {
            get => _language;
            set
            {
                if (value != _language)
                {
                    _language = value;
                    OnPropertyChanged("Language");
                }
            }
        }

        private int _maxTourists;
        public int MaxTourists
        {
            get => _maxTourists;
            set
            {
                if (value != _maxTourists)
                {
                    _maxTourists = value;
                    OnPropertyChanged("MaxTourists");
                }
            }
        }

        private int _duration;
        public int Duration
        {
            get => _duration;
            set
            {
                if (value != _duration)
                {
                    _duration = value;
                    OnPropertyChanged("Duration");
                }
            }
        }

        private string _keyPoints;
        public string KeyPoints
        {
            get => _keyPoints;
            set
            {
                if (value != _keyPoints)
                {
                    _keyPoints = value;
                    OnPropertyChanged("KeyPoints");
                }
            }
        }

        private string _pictures;
        public string Pictures
        {
            get => _pictures;
            set
            {
                if (value != _pictures)
                {
                    _pictures = value;
                    OnPropertyChanged("Pictures");
                }
            }
        }

        private DateTime date;
        private string _dates;
        public string Dates
        {
            get => _dates;
            set
            {
                if (value != _dates)
                {
                    _dates = value;
                    OnPropertyChanged("Dates");
                }
            }
        }

        public string Error => null;

        private Regex _NameRegex = new Regex("[A-Za-z0-9-]+ [A-Za-z0-9-]+");
        private Regex _LocationRegex = new Regex("([A-Za-z]+( [A-Za-z]+)*), ([A-Za-z]+( [A-Za-z]+)*)");
        private Regex _KeyPointsRegex = new Regex("([A-Za-z]+( [A-Za-z]+)*)(, [A-Za-z]+( [A-Za-z]+)*)+");


        public string this[string columnName]
        {
            get
            {
                if (columnName == "Title")
                {
                    if (string.IsNullOrEmpty(Title))
                        return "Title is required";

                }
                else if (columnName == "Description")
                {
                    if (string.IsNullOrEmpty(Description))
                        return "Description is required";

                }
                
                else if (columnName == "Location")
                {
                    if (string.IsNullOrEmpty(Location))
                        return "Location is required";

                    Match match = _LocationRegex.Match(Location);
                    if (!match.Success)
                        return "Location format not good. Try again.";

                }
                else if (columnName == "Language")
                {
                    if (string.IsNullOrEmpty(Language))
                        return "Language is required";

                }
                
                else if (columnName == "Max number of Tourists")
                {
                    if (string.IsNullOrEmpty(MaxTourists.ToString()))
                        return "MaxTourists is required";

                    int i;
                    if (!int.TryParse(MaxTourists.ToString(), out i))
                        return "Format not good. Try again.";
                }
                else if (columnName == "Key Points")
                {
                    if (string.IsNullOrEmpty(KeyPoints))
                        return "Key Points are required";

                    Match match = _KeyPointsRegex.Match(KeyPoints);
                    if (!match.Success)
                        return "Key Points format not good. Try again.";

                }
                else if (columnName == "Date and Time")
                {
                    if (string.IsNullOrEmpty(Dates))
                        return "Date is required";

                    DateTime d;
                    if (!DateTime.TryParse(Dates, out d))
                        return "Invalid date format. Try again.";
                    date = d;
                }
                
                else if (columnName == "Duration")
                {
                    if (string.IsNullOrEmpty(Duration.ToString()))
                        return "Duration is required";

                    int i;
                    if (!int.TryParse(Duration.ToString(), out i))
                        return "Format not good. Try again.";
                }

                return "";
            }
        }

        private readonly string[] _validatedProperties = { "Title", "Desription", "Location", "Language", "Max number of Tourists", "Key Points", "Date and Time", "Duration" };

        public string IsValid
        {
            get
            {
                foreach (var property in _validatedProperties)
                {
                    if (this[property] != "")
                        return this[property];
                }

                return "";
            }
        }



        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public CreateTour(User user)
        {
            InitializeComponent();
            Title = "Create Tour";
            DataContext = this;
            LoggedInUser = user;
            _repository = new TourRepository();
        }

        public CreateTour(Tour selectedTour)
        {
            InitializeComponent();
            DataContext = this;
            Title = "View tour";
            txtTitle.IsEnabled = false;
            txtLocation.IsEnabled = false;
            txtDescription.IsEnabled = false;
            txtLanguage.IsEnabled = false;
            txtKeyPoints.IsEnabled = false;
            txtMaxTourists.IsEnabled = false;
            txtPictures.IsEnabled = false;
            txtDuration.IsEnabled = false;
            txtDate.IsEnabled = false;
            //btnSave.Visibility = Visibility.Collapsed;
            SelectedTour = selectedTour;
            Title = selectedTour.Title;
            Duration = selectedTour.Duration;
            Location = selectedTour.Location; 
            Description = selectedTour.Description;
            Language = selectedTour.Language;

            
            
           
            MaxTourists = selectedTour.MaxTourists;

            //Pictures = selectedTour.Pictures;
            _repository = new TourRepository();
        }

        public CreateTour(Tour selectedTour, User user)
        {
            InitializeComponent();
            DataContext = this;
            Title = "//";
            LoggedInUser = user;
            SelectedTour = selectedTour;
            //Text = selectedTour.Text;
            _repository = new TourRepository();
        }

      /*  private void SaveComment(object sender, RoutedEventArgs e)
        { 
            Comment newComment = new Comment(DateTime.Now, Text, LoggedInUser);
            Comment savedComment = _repository.Save(newComment);
            CommentsOverview.Comments.Add(savedComment);
            
            Close();
        }*/

        private void Cancel(object sender, RoutedEventArgs e)
        {
            Close();
        }
       
    }
}
