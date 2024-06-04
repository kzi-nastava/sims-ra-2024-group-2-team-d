using BookingApp.Domain.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Dto
{
    public class TouristDTO: INotifyPropertyChanged
    {
        private string name;
        public string Name
        {
            get => name;
            set
            {
                if (value != name)
                {
                    if (Name != null && LastName != null && Age > 0)
                    {
                        IsPlusButtonEnabled = true;
                        OnPropertyChanged("IsPlusButtonEnabled");
                    }
                    name = value;
                    OnPropertyChanged("Name");
                }
            }
        }

        private string lastName;
        public string LastName
        {
            get => lastName;
            set
            {
                if (value != lastName)
                {
                    if(Name != null && LastName != null && Age > 0)
                    {
                        IsPlusButtonEnabled = true;
                        OnPropertyChanged("IsPlusButtonEnabled");
                    }
                    lastName = value;
                    OnPropertyChanged("LastName");
                }
            }
        }

        private int age;

        private bool isPlusButtonEnabled;
        public bool IsPlusButtonEnabled
        {
            get => isPlusButtonEnabled;
            set
            {
                if (isPlusButtonEnabled != value)
                {
                    if (Name != null && LastName != null && Age > 0)
                    {
                        IsPlusButtonEnabled = true;
                        OnPropertyChanged("IsPlusButtonEnabled");
                    }
                    isPlusButtonEnabled = value;
                    OnPropertyChanged(nameof(IsPlusButtonEnabled));
                }
            }
        }

        public int Age
        {
            get => age;
            set
            {
                if (value != age)
                {
                    age = value;
                    OnPropertyChanged("Age");
                }
            }
        }

        public TouristDTO()
        {
            isPlusButtonEnabled = false;
        }

        public TouristDTO(Tourist tourist)
        {
            Name = tourist.Name;
            LastName = tourist.LastName;
            Age = tourist.Age;
            isPlusButtonEnabled = false;
        }


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
