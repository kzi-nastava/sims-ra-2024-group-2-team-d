using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Services
{
    public class ComplexTourRequestService
    {
        public IComplexTourRequestRepository ComplexTourRequestReposotry {  get; set; }
        public ComplexTourRequestService(IComplexTourRequestRepository complexTourRequestRepository) {
            ComplexTourRequestReposotry = complexTourRequestRepository;
        }

        public ComplexTourRequest Save(ComplexTourRequest complexTourRequest)
        {
            return ComplexTourRequestReposotry.Save(complexTourRequest);
        }

        public List<ComplexTourRequest> GetAll()
        {
            return ComplexTourRequestReposotry.GetAll();
        }

        public List<ComplexTourRequest> GetAllForGuide(User user)
        {
            return GetAll().Where(c => c.TourRequests.All(t => t.GuideId != user.Id)).ToList();
        }


    }
}
