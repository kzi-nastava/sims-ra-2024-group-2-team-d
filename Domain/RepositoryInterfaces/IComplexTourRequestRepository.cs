using BookingApp.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.RepositoryInterfaces
{
    public interface IComplexTourRequestRepository
    {
        public ComplexTourRequest Save(ComplexTourRequest complexTourRequest);

        public List<ComplexTourRequest> GetAll();

        public List<ComplexTourRequest> GetByUserId(int userId);
        public ComplexTourRequest Update(ComplexTourRequest complexTourRequest);


    }
}
