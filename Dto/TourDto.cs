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

namespace BookingApp.Dto
{
    public class TourDto : INotifyPropertyChanged, IDataErrorInfo
    {

        public Tour ToModel()
        {

            Tour tour = new Tour(this.UserId, this.Title, this.Description, this.Location, this.Language, this.MaxTourists, this.KeyPoints, this.Dates, Duration, Pictures);
            return tour;

        }

        public int UserId;
        //public List<String> Pictures;

        public User LoggedInUser { get; set; }

        public Tour SelectedTour { get; set; }

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
        private Regex _DatesRegex = new Regex("(?:0?[1-9]|[12]\\d|3[01])/(0[1-9]|1[0-2])/((?:19|20)\\d{2}) (?:[01]\\d|2[0-3]):(?:[0-5]\\d)(?:,(?:0?[1-9]|[12]\\d|3[01])/(0[1-9]|1[0-2])/((?:19|20)\\d{2}) (?:[01]\\d|2[0-3]):(?:[0-5]\\d))*");
        private Regex _PicturesRegex = new Regex("(https?|ftp):\\/\\/[^\\s\\/$.?#].[^\\s]*(?:,\\s*(https?|ftp):\\/\\/[^\\s\\/$.?#].[^\\s]*)*");


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
                        return "Location format not good. Try 'City, State'.";

                }
                else if (columnName == "Language")
                {
                    if (string.IsNullOrEmpty(Language))
                        return "Language is required";

                }

                else if (columnName == "MaxTourists")
                {
                    if (string.IsNullOrEmpty(MaxTourists.ToString()))
                        return "Max number of Tourists is required";

                    int i;
                    if (!int.TryParse(MaxTourists.ToString(), out i))
                        return "Format not good. Try again.";

                    if (int.TryParse(MaxTourists.ToString(), out i) && i == 0)
                        return "Max number of Tourists can't be 0.";
                }
                else if (columnName == "KeyPoints")
                {
                    if (string.IsNullOrEmpty(KeyPoints))
                        return "Key Points are required";

                    Match match = _KeyPointsRegex.Match(KeyPoints);
                    if (!match.Success)
                        return "You must enter at least 2 key points";

                }
                else if (columnName == "Dates")
                {
                    if (string.IsNullOrEmpty(Dates))
                        return "Date(s) and Time(s) are required";

                    Match match = _DatesRegex.Match(Dates);
                    if (!match.Success)
                        return "Invalid date/time format. Try dd/MM/yyyy HH:mm(, ...)";

                }

                else if (columnName == "Duration")
                {
                    if (string.IsNullOrEmpty(Duration.ToString()))
                        return "Duration is required";

                    int i;
                    if (!int.TryParse(Duration.ToString(), out i))
                        return "Format not good. Try again.";

                    if (int.TryParse(Duration.ToString(), out i) && i==0)
                        return "Duration can't be 0.";
                }

                else if (columnName == "Pictures")
                {
                    if (string.IsNullOrEmpty(Pictures))
                        return "At least one picture is required";

                    Match match = _PicturesRegex.Match(Pictures);
                    if (!match.Success)
                        return "Please enter an URL address";

                }

                return "";
            }
        }

        private readonly string[] _validatedProperties = { "Title", "Desription", "Location", "Language", "MaxTourists", "KeyPoints", "Dates", "Duration", "Pictures" };

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

    }
}
