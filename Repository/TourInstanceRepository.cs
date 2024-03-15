using BookingApp.Model;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Repository
{
    public class TourInstanceRepository
    {
        private const string FilePath = "../../../Resources/Data/tourInstance.csv";

        private readonly Serializer<TourInstance> _serializer;

        private List<TourInstance> _tourInstance;

        public TourInstanceRepository()
        {
            _serializer = new Serializer<TourInstance>();
            _tourInstance = _serializer.FromCSV(FilePath);
        }

        public List<TourInstance> GetAll()
        {
            return _serializer.FromCSV(FilePath);
        }
    }
}
