using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Serializer;


namespace BookingApp.Repository
{
    public class ComplexTourRequestRepository: IComplexTourRequestRepository
    {
        private const string FilePath = "../../../Resources/Data/complexTourRequests.csv";

        private readonly Serializer<ComplexTourRequest> _serializer;

        private List<ComplexTourRequest> _complexTourRequests;

        public ComplexTourRequestRepository()
        {
            _serializer = new Serializer<ComplexTourRequest>();
            _complexTourRequests = _serializer.FromCSV(FilePath);
        }

        public int NextId()
        {
            _complexTourRequests = _serializer.FromCSV(FilePath);
            if (_complexTourRequests.Count < 1)
            {
                return 1;
            }
            return _complexTourRequests.Max(c => c.Id) + 1;
        }
        public ComplexTourRequest Save(ComplexTourRequest complexTourRequest)
        {
            complexTourRequest.Id = NextId();
            _complexTourRequests = _serializer.FromCSV(FilePath);
            _complexTourRequests.Add(complexTourRequest);
            _serializer.ToCSV(FilePath, _complexTourRequests);
            return complexTourRequest;
        }

        public List<ComplexTourRequest> GetAll()
        {
            return _complexTourRequests;
        }

        
    }
}
